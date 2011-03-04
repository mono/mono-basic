' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))> _
Public Class ExternalProcessExecutor
    Private m_Executable As String
    Private m_UnexpandedCmdLine As String
    Private m_ExpandedCmdLine As String

    Private m_StdOut As New System.Text.StringBuilder
    Private m_StdErr As New System.Text.StringBuilder
    Private m_TimeOut As Integer
    Private m_TimedOut As Boolean
    Private m_ExitCode As Integer
    Private m_WorkingDirectory As String

    Private m_UseTemporaryExecutable As Boolean

    Private m_LastWriteDate As Date
    Private m_Version As FileVersionInfo

    Private m_DirsToDelete As Generic.List(Of String)
    Private m_Retries As Integer
    Private m_StdOutEvent As New Threading.ManualResetEvent(False)
    Private m_StdErrEvent As New Threading.ManualResetEvent(False)

    ReadOnly Property FileVersion() As FileVersionInfo
        Get
            Return m_Version
        End Get
    End Property

    ReadOnly Property LastWriteDate() As Date
        Get
            Return m_LastWriteDate
        End Get
    End Property

    Property WorkingDirectory() As String
        Get
            Return m_WorkingDirectory
        End Get
        Set(ByVal value As String)
            m_WorkingDirectory = value
        End Set
    End Property

    ''' <summary>
    ''' If this is true the executable is copied to a temporary file before it is executed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UseTemporaryExecutable() As Boolean
        Get
            Return m_UseTemporaryExecutable
        End Get
        Set(ByVal value As Boolean)
            m_UseTemporaryExecutable = value
        End Set
    End Property

    ReadOnly Property ExitCode() As Integer
        Get
            Return m_ExitCode
        End Get
    End Property

    ReadOnly Property TimedOut() As Boolean
        Get
            Return m_TimedOut
        End Get
    End Property

    ReadOnly Property TimeOut() As Integer
        Get
            Return m_TimeOut
        End Get
    End Property

    ReadOnly Property Executable() As String
        Get
            Return m_Executable
        End Get
    End Property

    ''' <summary>
    ''' The StdOut of the verification, if applicable.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StdOut() As String
        Get
            Return m_StdOut.ToString & vbNewLine & m_StdErr.ToString
        End Get
        Set(ByVal value As String)
            m_StdOut = New System.Text.StringBuilder(value)
        End Set
    End Property

    ReadOnly Property UnexpandedCommandLine() As String
        Get
            Return m_UnexpandedCmdLine
        End Get
    End Property

    ReadOnly Property ExpandedCmdLine() As String
        Get
            Return m_ExpandedCmdLine
        End Get
    End Property

    ''' <summary>
    ''' Creates a new executor.
    ''' </summary>
    ''' <param name="Executable">The program to use.</param>
    ''' <param name="ExpandableCommandLine">The parameters to the program. 
    ''' Environment variables are also expanded.</param>
    ''' <remarks></remarks>
    Sub New(ByVal Executable As String, ByVal ExpandableCommandLine As String, Optional ByVal WorkingDirectory As String = "", Optional ByVal TimeOut As Integer = 30000)
        m_UnexpandedCmdLine = ExpandableCommandLine
        m_Executable = Environment.ExpandEnvironmentVariables(Executable)
        m_ExpandedCmdLine = m_UnexpandedCmdLine
        m_ExpandedCmdLine = Environment.ExpandEnvironmentVariables(m_ExpandedCmdLine)
        m_TimeOut = TimeOut * 4
        m_WorkingDirectory = WorkingDirectory
        'If IO.File.Exists(m_Executable) Then m_Version = FileVersionInfo.GetVersionInfo(m_Executable)
    End Sub

    Sub ExpandCmdLine()
        ExpandCmdLine(New String() {}, New String() {})
    End Sub

    ''' <summary>
    ''' Expand the commandline.
    ''' Arguments are automatically surrounded by quotes if they contain spaces.
    ''' </summary>
    ''' <param name="Arguments"></param>
    ''' <param name="Values"></param>
    ''' <remarks></remarks>
    Sub ExpandCmdLine(ByVal Arguments() As String, ByVal Values() As String)
        Debug.Assert(Arguments.Length = Values.Length)
        For i As Integer = 0 To Arguments.Length - 1
            Dim value As String = Values(i)
            If value Is Nothing Then Continue For
            If value.StartsWith("""") = False AndAlso value.EndsWith("""") = False AndAlso value.IndexOf(" "c) >= 0 Then
                value = """" & value & """"
            End If
            m_ExpandedCmdLine = m_ExpandedCmdLine.Replace(Arguments(i), value)
        Next
    End Sub

    Private Sub OutputReader(ByVal state As Object)
        Try
            Dim line As String
            Dim m_Process As Process = DirectCast(state, Process)
            line = m_Process.StandardOutput.ReadLine
            While line IsNot Nothing
                m_StdOut.AppendLine(line)
                line = m_Process.StandardOutput.ReadLine
            End While
            m_StdOutEvent.Set()
        Catch ex As OutOfMemoryException
            m_StdOut.AppendLine(ex.Message)
        End Try
    End Sub

    Private Sub ErrorReader(ByVal state As Object)
        Try
            Dim m_Process As Process = DirectCast(state, Process)
            Dim line As String
            line = m_Process.StandardError.ReadLine
            While line IsNot Nothing
                m_StdErr.AppendLine(line)
                line = m_Process.StandardError.ReadLine
            End While
            m_StdErrEvent.Set()
        Catch ex As OutOfMemoryException
            m_StdOut.AppendLine(ex.Message)
        End Try
    End Sub


    Public Function RunProcess() As Boolean
        Dim process As New Process
        Dim realExecutable As String

        Try
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.UseShellExecute = False
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.WorkingDirectory = m_WorkingDirectory

            If Not String.IsNullOrEmpty(m_WorkingDirectory) AndAlso Not IO.Directory.Exists(m_WorkingDirectory) Then
                IO.Directory.CreateDirectory(m_WorkingDirectory)
            End If

            If IO.File.Exists(m_Executable) = False Then
                'm_StdOut.Append("Executable '" & m_Executable & "' does not exist.")
            Else
                m_LastWriteDate = IO.File.GetLastWriteTime(m_Executable)
                If m_Version Is Nothing Then
                    m_Version = FileVersionInfo.GetVersionInfo(m_Executable)
                End If
            End If

            If m_UseTemporaryExecutable Then
                Dim srcdir As String
                Dim tmpdir As String
                Dim sourcefile As String

                tmpdir = System.IO.Path.GetTempFileName()
                IO.File.Delete(tmpdir)
                IO.Directory.CreateDirectory(tmpdir)
                If m_DirsToDelete Is Nothing Then m_DirsToDelete = New Generic.List(Of String)
                m_DirsToDelete.Add(tmpdir)

                sourcefile = m_Executable
                srcdir = IO.Path.GetDirectoryName(sourcefile)

                Dim patterns() As String = New String() {"*.exe", "*.dll", "*.pdb"}
                For Each pattern As String In patterns
                    For Each file As String In IO.Directory.GetFiles(srcdir, pattern)
                        IO.File.Copy(file, IO.Path.Combine(tmpdir, IO.Path.GetFileName(file)))
                    Next
                Next

                realExecutable = IO.Path.Combine(tmpdir, IO.Path.GetFileName(sourcefile))
            Else
                realExecutable = m_Executable
            End If

            If Helper.IsOnMono AndAlso realExecutable.EndsWith(".exe") Then
                process.StartInfo.FileName = "mono"
                process.StartInfo.Arguments = "--debug " & realExecutable & " " & m_ExpandedCmdLine
            Else

                process.StartInfo.FileName = realExecutable
                process.StartInfo.Arguments = m_ExpandedCmdLine
            End If
            'Console.WriteLine("Executing: FileName={0}, Arguments={1}", process.StartInfo.FileName, process.StartInfo.Arguments)

            process.Start()
            Try
                'This may fail if the process exits before we get to this line
                process.PriorityClass = ProcessPriorityClass.Idle
            Catch
            End Try

            Threading.ThreadPool.QueueUserWorkItem(AddressOf OutputReader, process)
            Threading.ThreadPool.QueueUserWorkItem(AddressOf ErrorReader, process)

            m_TimedOut = Not process.WaitForExit(m_TimeOut)
            If m_TimedOut Then
                process.Kill()
            End If
            m_ExitCode = process.ExitCode
            m_StdErrEvent.WaitOne()
            m_StdOutEvent.WaitOne()
            process.Close()
            'process.CancelOutputRead()
            'RemoveHandler process.OutputDataReceived, AddressOf OutputReader
        Catch ex As Exception
            m_ExitCode = Integer.MinValue
            m_StdOut.AppendLine("Exception while executing process: ")
            m_StdOut.AppendLine(ex.Message)
            m_StdOut.AppendLine(ex.StackTrace)
        Finally
            If process IsNot Nothing Then process.Dispose()
            DeleteFilesAndDirectories(Me)
        End Try
        Return True
    End Function

    Private Sub DeleteFilesAndDirectories(ByVal state As Object)
        'Don't retry indefinitively
        If m_Retries > 10 Then
            Debug.WriteLine("Too many file/directory deletion retries, bailing out")
            Return
        End If

        'Don't retry too often, wait a bit.
        If m_Retries > 0 Then Threading.Thread.Sleep(m_Retries * 100)

        If m_DirsToDelete IsNot Nothing Then
            For i As Integer = m_DirsToDelete.Count - 1 To 0
                Try
                    IO.Directory.Delete(m_DirsToDelete(i), True)
                    m_DirsToDelete.RemoveAt(i)
                Catch ex As Exception 'Ignore any errors whatsoever
                    Debug.WriteLine(m_Retries & " Could not delete directory: " & ex.ToString)
                End Try
            Next
        End If

        If m_DirsToDelete IsNot Nothing AndAlso m_DirsToDelete.Count > 0 Then
            m_Retries += 1
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf DeleteFilesAndDirectories, Me)
        End If
    End Sub

End Class

