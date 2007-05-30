' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

    Private m_StdOut As String
    Private m_TimeOut As Integer
    Private m_TimedOut As Boolean
    Private m_ExitCode As Integer
    Private m_WorkingDirectory As String

    Private m_UseTemporaryExecutable As Boolean
    Private m_Stats As TestStatistics

    Private m_LastWriteDate As Date
    Private m_Version As FileVersionInfo

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

    ReadOnly Property Statistics() As TestStatistics
        Get
            Return m_Stats
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
    ReadOnly Property StdOut() As String
        Get
            Return m_StdOut
        End Get
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
        m_TimeOut = TimeOut
        m_WorkingDirectory = WorkingDirectory
        If IO.File.Exists(m_Executable) Then m_Version = FileVersionInfo.GetVersionInfo(m_Executable)
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
            If value.StartsWith("""") = False AndAlso value.EndsWith("""") = False AndAlso value.IndexOf(" "c) >= 0 Then
                value = """" & value & """"
            End If
            m_ExpandedCmdLine = m_ExpandedCmdLine.Replace(Arguments(i), value)
        Next
    End Sub

    Private Sub OutputReader(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        m_StdOut &= e.Data & vbNewLine
    End Sub

    Public Function RunProcess() As Boolean
        Dim process As New Process
        Try
            If Helper.IsOnMono Then
                process.StartInfo.FileName = "mono"
                process.StartInfo.Arguments = "--debug " & m_Executable & " " & m_ExpandedCmdLine
            Else
                process.StartInfo.FileName = m_Executable
                process.StartInfo.Arguments = m_ExpandedCmdLine
            End If
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.UseShellExecute = False
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.WorkingDirectory = m_WorkingDirectory

            If IO.File.Exists(m_Executable) = False Then
                m_StdOut = "Executable '" & m_Executable & "' does not exist."
                Return False
            End If

            m_LastWriteDate = IO.File.GetLastWriteTime(m_Executable)
            If m_Version Is Nothing Then
                m_Version = FileVersionInfo.GetVersionInfo(m_Executable)
            End If

            If m_UseTemporaryExecutable Then
                Dim tmpdir As String
                Dim tmpsourcefile As String
                Dim tmppdbfile As String
                Dim sourcefile As String
                Dim sourcepdbfile As String

                tmpdir = System.IO.Path.GetTempFileName()
                IO.File.Delete(tmpdir)
                IO.Directory.CreateDirectory(tmpdir)

                sourcefile = process.StartInfo.FileName
                sourcepdbfile = IO.Path.Combine(IO.Path.GetDirectoryName(sourcefile), IO.Path.GetFileNameWithoutExtension(process.StartInfo.FileName) & ".pdb")

                tmpsourcefile = IO.Path.Combine(tmpdir, IO.Path.GetFileName(sourcefile))
                tmppdbfile = IO.Path.Combine(tmpdir, IO.Path.GetFileName(sourcepdbfile))

                System.IO.File.Copy(sourcefile, tmpsourcefile, True)
                If IO.File.Exists(sourcepdbfile) Then
                    IO.File.Copy(sourcepdbfile, tmppdbfile)
                End If
                process.StartInfo.FileName = tmpsourcefile
            End If

            'Console.WriteLine("Executing: FileName={0}, Arguments={1}", process.StartInfo.FileName, process.StartInfo.Arguments)

            process.Start()
            Try
                'This may fail if the process exits before we get to this line
                process.PriorityClass = ProcessPriorityClass.Idle
            Catch
            End Try

            AddHandler process.OutputDataReceived, AddressOf OutputReader
            process.BeginOutputReadLine()

            m_TimedOut = Not process.WaitForExit(m_TimeOut)
            If m_TimedOut Then
                process.Kill()
            End If
            m_ExitCode = process.ExitCode
            m_Stats = New TestStatistics(process)

            process.Close()
            'process.CancelOutputRead()
            'RemoveHandler process.OutputDataReceived, AddressOf OutputReader

            If m_UseTemporaryExecutable Then
                Try
                    System.IO.Directory.Delete(IO.Path.GetDirectoryName(process.StartInfo.FileName), True)
                Catch ex As UnauthorizedAccessException
                    'Ignore this exception.
                Catch ex As IO.IOException
                    'Ignore this exception
                End Try
            End If
        Catch ex As Exception
            m_ExitCode = Integer.MinValue
            m_StdOut = "Exception while executing process: " & Environment.NewLine & ex.Message & Environment.NewLine & ex.StackTrace
        Finally
            If process IsNot Nothing Then process.Dispose()
        End Try
        Return True
    End Function
End Class
