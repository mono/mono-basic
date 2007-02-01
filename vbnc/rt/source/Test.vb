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

<Serializable()> _
Public Class Test
    Private Const PEVerifyPath As String = "%programfiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\PEVerify.exe"
    ''' <summary>
    ''' The files that contains this test.
    ''' </summary>
    Private m_Files As New Generic.List(Of String)

    ''' <summary>
    ''' The name of the test
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Name As String

    ''' <summary>
    ''' The base path of where the code file(s) are
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BasePath As String

    ''' <summary>
    ''' The path of where the output files are
    ''' </summary>
    ''' <remarks></remarks>
    Private m_OutputPath As String

    ''' <summary>
    ''' The response file, if any.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ResponseFile As String
    Private m_RspFile As String
    Private m_DefaultRspFile As String

    ''' <summary>
    ''' The result of the test
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Result As Results
    Private m_OldResult As Results

    ''' <summary>
    ''' The test container
    ''' </summary>
    ''' <remarks></remarks>
    <NonSerialized()> Private m_Parent As Tests

    ''' <summary>
    ''' How long did the test take?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TestDuration As TimeSpan

    Private m_Verifications As New Generic.List(Of VerificationBase)

    ''' <summary>
    ''' The compilation using our compiler.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Compilation As ExternalProcessVerification

    Public Const OutputExtension As String = ".output.xml"
    Public Const VerifiedExtension As String = ".verified.xml"
    Public Const OutputPattern As String = ".*" & OutputExtension
    Public Const VerifiedPattern As String = ".*" & VerifiedExtension
    Public DefaultOutputPath As String = "testoutput" & System.IO.Path.DirectorySeparatorChar

    Private m_Target As String
    Private m_NoConfig As Boolean

    Private m_LastRun As Date

    Private m_Tag As Object

    Private m_IsNegativeTest As Boolean
    Private m_NegativeError As Integer
    Private m_LoadedOldResults As Boolean

    Public Event Executed(ByVal Sender As Test)
    Public Event Executing(ByVal Sender As Test)
    Public Event Changed(ByVal Sender As Test)

    Private m_Compiler As String
    Private m_AC As String
    Private Shared m_NegativeRegExpTest As New System.Text.RegularExpressions.Regex("^\d+(-\d*)?(\s+.*)?$", System.Text.RegularExpressions.RegexOptions.Compiled)

    Property AC() As String
        Get
            Return m_AC
        End Get
        Set(ByVal value As String)
            m_AC = value
        End Set
    End Property

    Property Compiler() As String
        Get
            Return m_Compiler
        End Get
        Set(ByVal value As String)
            m_Compiler = value
        End Set
    End Property

    Function GetOldResults() As Generic.List(Of OldResult)
        Dim result As New Generic.List(Of OldResult)

        Dim files() As String = {}
        Try
            files = IO.Directory.GetFiles(Me.OutputPath, Me.Name & ".*.testresult")
        Catch io As IO.IOException

        End Try

        For Each file As String In files
            result.Add(New OldResult(file))
        Next
        Return result
    End Function

    Public ReadOnly Property IsNegativeTest() As Boolean
        Get
            Return m_IsNegativeTest
        End Get
    End Property

    ReadOnly Property NegativeError() As Integer
        Get
            Return m_NegativeError
        End Get
    End Property

    Property Tag() As Object
        Get
            Return m_Tag
        End Get
        Set(ByVal value As Object)
            m_Tag = value
        End Set
    End Property

    ReadOnly Property LastRun() As Date
        Get
            Return m_LastRun
        End Get
    End Property

#If DEBUG Then
    Private id As Integer = Helper.nextID()
#End If

    Sub WriteToXML(ByVal xml As Xml.XmlWriter)
        xml.WriteStartElement(Me.GetType.ToString)
        xml.WriteElementString("Name", m_Name)
        For Each file As String In m_Files
            xml.WriteElementString("File", file)
        Next
        xml.WriteElementString("BasePath", m_BasePath)
        xml.WriteElementString("OutputPath", m_OutputPath)
        xml.WriteElementString("ResponseFile", m_ResponseFile)
        xml.WriteElementString("ExitCode", ExitCode.ToString)
        xml.WriteElementString("Result", m_Result.ToString)
        xml.WriteElementString("StdOut", m_Compilation.Process.StdOut)
        If Statistics IsNot Nothing Then Statistics.WriteToXML(xml)
        xml.WriteEndElement()
    End Sub

    Sub LoadOldResults()
        If m_LoadedOldResults Then Return
        Dim oldresults As Generic.List(Of OldResult)
        oldresults = Me.GetOldResults
        If oldresults.Count > 0 Then
            m_OldResult = oldresults.Item(oldresults.Count - 1).Result
            If m_OldResult = Results.Failed Then
                For i As Integer = oldresults.Count - 1 To 0 Step -1
                    If oldresults(i).Result = Results.Success Then m_OldResult = Results.Regressed
                Next
            End If
        Else
            m_OldResult = Results.NotRun
        End If
        m_LoadedOldResults = True
        RaiseEvent Changed(Me)
    End Sub

    ReadOnly Property TestDuration() As TimeSpan
        Get
            Return m_TestDuration
        End Get
    End Property

    ''' <summary>
    ''' The statistics for the test. Might be nothing (if test hasn't been run.)
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Statistics() As TestStatistics
        Get
            If m_Compilation Is Nothing OrElse m_Compilation.Process Is Nothing Then
                Return Nothing
            End If
            Return m_Compilation.Process.Statistics
        End Get
    End Property

    ReadOnly Property Verifications() As VerificationBase()
        Get
            Return m_Verifications.ToArray
        End Get
    End Property

    ''' <summary>
    ''' The test container
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Friend ReadOnly Property Parent() As Tests
        Get
            Return m_Parent
        End Get
    End Property

    ''' <summary>
    ''' Has this test been run?
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Run() As Boolean
        Get
            Return m_Result > Results.NotRun
        End Get
    End Property

    ''' <summary>
    ''' The base path of where the code file(s) are
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property BasePath() As String
        Get
            Return m_BasePath
        End Get
    End Property

    ''' <summary>
    ''' The path of where the output files are
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property OutputPath() As String
        Get
            Return m_OutputPath
        End Get
    End Property

    ''' <summary>
    ''' The files that contains this test.
    ''' </summary>
    ReadOnly Property Files() As Generic.List(Of String)
        Get
            Return m_Files
        End Get
    End Property

    ''' <summary>
    ''' The StdOut of the test
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property StdOut() As String
        Get
            If m_Compilation IsNot Nothing Then
                Return m_Compilation.Process.StdOut
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' The exit code of the compilation
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ExitCode() As Integer
        Get
            If m_Compilation Is Nothing OrElse m_Compilation.Process Is Nothing Then
                Return 0
            End If
            Return m_Compilation.Process.ExitCode
        End Get
    End Property

    ''' <summary>
    ''' Get the files as a string with the filenames quoted.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFileList() As String
        Dim result As String = ""
        For Each file As String In m_Files
            result = result & " """ & file & """"
        Next
        Return result
    End Function

    ''' <summary>
    ''' The response file, if any.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ResponseFile() As String
        Get
            Return m_ResponseFile
        End Get
    End Property

    ReadOnly Property RspFile() As String
        Get
            Return m_RspFile
        End Get
    End Property

    ''' <summary>
    ''' The name of the test.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

    ''' <summary>
    ''' The result of the test.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Success() As Boolean
        Get
            Return m_Result = Results.Success
        End Get
    End Property

    ''' <summary>
    ''' THe result of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Result() As Results
        Get
            Return m_Result
        End Get
    End Property

    ReadOnly Property OldResult() As Results
        Get
            Return m_OldResult
        End Get
    End Property

    ''' <summary>
    ''' Has this test multiple files?
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsMultiFile() As Boolean
        Get
            Return m_Files.Count > 1
        End Get
    End Property

    Function GetOutputAssembly() As String
        Return IO.Path.Combine(Me.OutputPath, Name & "." & m_Target)
    End Function

    Function GetOutputVBCAssembly() As String
        Return IO.Path.Combine(Me.OutputPath, Name & "_vbc." & m_Target)
    End Function

    ''' <summary>
    ''' Get the xml output files.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOutputFiles() As String()
        Dim result As String()
        If IO.Directory.Exists(OutputPath) Then
            result = New String() {} 'IO.Directory.GetFiles(OutputPath, Name & OutputPattern)
        Else
            result = New String() {}
        End If
        Return result
    End Function

    Function GetVerifiedFiles() As String()
        Dim result As String()
        If IO.Directory.Exists(OutputPath) Then
            result = new String(){}'IO.Directory.GetFiles(OutputPath, Name & VerifiedPattern)
        Else
            result = New String() {}
        End If
        Return result
    End Function

    ''' <summary>
    ''' Returns a commandline to execute this test. Does not include the compiler executable.
    ''' Arguments are quoted, ready to execute.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTestCommandLine(Optional ByVal ForVBC As Boolean = False) As String
        Dim result As New System.Text.StringBuilder

        Initialize()

        result.Append(" "c)
        For Each str As String In GetTestCommandLineArguments(ForVBC)
            If str.Contains(" "c) Then
                result.Append("""" & str & """")
            Else
                result.Append(str)
            End If
            result.Append(" "c)
        Next

        Return result.ToString
    End Function

    ''' <summary>
    ''' Returns the commandline arguments to execute this test. Does not include the compiler executable.
    ''' Arguments are not quoted!
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTestCommandLineArguments(Optional ByVal ForVBC As Boolean = False) As String()
        Dim result As New Generic.List(Of String)

        Initialize()

        'First option is always the /out: argument.
        Const OutArgument As String = "-out:{0}"
        If ForVBC Then
            result.Add(String.Format(OutArgument, GetOutputVBCAssembly))
        Else
            result.Add(String.Format(OutArgument, GetOutputAssembly))
        End If

        'If there is one unique .rsp file, use it
        If m_RspFile <> "" Then
            result.Add("@" & m_RspFile)
        Else
            'otherwise add the file, the default response file and the extra .response file if any.
            result.AddRange(CType(m_Files, Generic.IEnumerable(Of String)))
            result.Add("-libpath:" & m_BasePath)

            If m_DefaultRspFile <> "" Then
                result.Add("@" & m_DefaultRspFile)
            End If

            If m_ResponseFile <> "" Then result.Add("@" & m_ResponseFile)
        End If

        If m_NoConfig Then
            result.Add("-noconfig")
        End If

        Return result.ToArray()
    End Function

    ReadOnly Property FailedVerification() As VerificationBase
        Get
            If m_Verifications Is Nothing Then Return Nothing
            For Each v As VerificationBase In m_Verifications
                If v.Result = False Then Return v
            Next
            Return Nothing
        End Get
    End Property

    ReadOnly Property FailedVerificationMessage() As String
        Get
            Dim tmp As VerificationBase = FailedVerification
            If tmp IsNot Nothing Then Return tmp.DescriptiveMessage
            Return ""
        End Get
    End Property

    Sub Initialize()
        Dim rsp As String
        Dim contents As String

        rsp = IO.Path.Combine(m_BasePath, Name) & ".response"
        If IO.File.Exists(rsp) Then m_ResponseFile = rsp Else m_ResponseFile = ""
        rsp = IO.Path.Combine(m_BasePath, Name) & ".rsp"
        If IO.File.Exists(rsp) Then m_RspFile = rsp Else m_RspFile = ""
        rsp = IO.Path.Combine(m_BasePath, "all.rsp")
        If IO.File.Exists(rsp) Then m_DefaultRspFile = rsp Else m_DefaultRspFile = ""

        'Find the target of the test (exe, winexe, library, module)
        m_Target = "exe" 'default target.
        If m_RspFile <> "" Then
            contents = IO.File.ReadAllText(m_RspFile)
            m_Target = GetTarget(contents, m_Target)
            m_NoConfig = IsNoConfig(contents)
        Else
            If m_DefaultRspFile <> "" Then
                contents = IO.File.ReadAllText(m_DefaultRspFile)
                m_Target = GetTarget(contents, m_Target)
                m_NoConfig = IsNoConfig(contents) OrElse m_NoConfig
            End If
            If m_ResponseFile <> "" Then
                contents = IO.File.ReadAllText(m_ResponseFile)
                m_Target = GetTarget(contents, m_Target)
                m_NoConfig = IsNoConfig(contents) OrElse m_NoConfig
            End If
        End If
    End Sub

    ReadOnly Property IsDirty() As Boolean
        Get
            If IO.File.Exists(Me.GetOutputAssembly) = False Then Return True
            If AreExecutablesDirty Then Return True
            If IsSourceDirty Then Return True
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Returns true if any of the verification executables has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AreExecutablesDirty() As Boolean
        Get
            Dim fileDate As Date
            Dim filesToCheck As New Generic.List(Of String)

            If Parent.VBNCPath <> "" Then filesToCheck.Add(Parent.VBNCPath)
            If Parent.VBCPath <> "" Then filesToCheck.Add(Parent.VBCPath)
            If PEVerifyPath <> "" Then filesToCheck.Add(PEVerifyPath)
            If GetACPath <> "" Then filesToCheck.Add(GetACPath)

            For Each item As String In filesToCheck
                fileDate = IO.File.GetLastWriteTime(item)
                If fileDate > m_LastRun Then Return True
            Next
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Returns true if the vbc assembly is dirty as well (the source has changed).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsSourceDirty() As Boolean
        Get
            If IO.File.Exists(Me.GetOutputVBCAssembly) = False Then
                Return True
            ElseIf IO.File.GetLastWriteTime(Me.GetOutputVBCAssembly) < LastSourceWrite Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the last date of any source file or response file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property LastSourceWrite() As Date
        Get
            Dim lastDate As Date
            Dim fileDate As Date
            Dim filesToCheck As New Generic.List(Of String)

            filesToCheck.AddRange(m_Files)
            If m_ResponseFile <> "" Then filesToCheck.Add(m_ResponseFile)
            If m_RspFile <> "" Then filesToCheck.Add(m_RspFile)
            If m_DefaultRspFile <> "" Then filesToCheck.Add(m_DefaultRspFile)

            For Each item As String In filesToCheck
                fileDate = IO.File.GetLastWriteTime(item)
                If fileDate > lastDate Then
                    lastDate = fileDate
                End If
            Next
            Return lastDate
        End Get
    End Property

    Private ReadOnly Property GetACPath() As String
        Get
            If m_AC <> String.Empty Then Return m_AC
            Return IO.Path.GetFullPath("..\..\ac\bin\ac.exe".Replace("\", IO.Path.DirectorySeparatorChar))
        End Get
    End Property

    Function GetExecutor() As String
        Return IO.Path.GetFullPath("..\..\rt-execute\rt-execute.exe".Replace("\", IO.Path.DirectorySeparatorChar))
    End Function

    ''' <summary>
    ''' Returns true if new verifications have been created (only if source files has changed
    ''' or vbnc compiler has changed since last run).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateVerifications() As Boolean
        Initialize()

        If Me.Parent IsNot Nothing AndAlso (Me.Parent.SkipCleanTests AndAlso Me.IsDirty = False) Then Return False

        Dim vbnccmdline As String() = Helper.QuoteStrings(Me.GetTestCommandLineArguments(False))
        Dim vbccmdline As String() = Helper.QuoteStrings(Me.GetTestCommandLineArguments(True))

        Dim vbc As ExternalProcessVerification = Nothing
        Dim compiler As String = Nothing
        Dim vbccompiler As String = Nothing

        If Me.Parent IsNot Nothing Then
            vbccompiler = Me.Parent.VBCPath
        End If
        If vbccompiler <> String.Empty Then
            vbc = New ExternalProcessVerification(Me, vbccompiler, Join(vbccmdline, " "))
            vbc.Process.WorkingDirectory = m_BasePath
            vbc.Name = "VBC Compile (verifies that the test itself is correct)"
            If m_IsNegativeTest Then vbc.NegativeError = m_NegativeError
        End If

        compiler = Me.Compiler
        If compiler Is Nothing AndAlso Me.Parent IsNot Nothing Then
            compiler = Me.Parent.VBNCPath
        End If
        If compiler Is Nothing Then
            Throw New Exception("No compiler specified.")
        End If

        m_Compilation = New ExternalProcessVerification(Me, compiler, Join(vbnccmdline, " "))
        m_Compilation.Process.WorkingDirectory = m_BasePath
        m_Compilation.Name = "VBNC Compile"
        'm_Compilation.Process.UseTemporaryExecutable = True
        If m_IsNegativeTest Then m_Compilation.NegativeError = m_NegativeError

        m_Verifications.Clear()

        If vbc IsNot Nothing AndAlso IsSourceDirty Then m_Verifications.Add(vbc)
        m_Verifications.Add(m_Compilation)

        If m_IsNegativeTest = False Then
            If Me.m_Target = "exe" Then
                m_Verifications.Add(New ExternalProcessVerification(Me, Me.GetOutputVBCAssembly))
                m_Verifications(m_Verifications.Count - 1).Name = "Test executable verification"
            End If

            'm_Verifications.Add(New XMLVerifier(Me))
            'm_Verifications(m_Verifications.Count - 1).Name = "Xml verification"

            Dim ac As String
            ac = GetACPath
            If ac <> String.Empty AndAlso vbccompiler <> String.Empty AndAlso IO.File.Exists(ac) AndAlso IO.File.Exists(vbccompiler) Then
                m_Verifications.Add(New ExternalProcessVerification(Me, GetACPath, "%OUTPUTASSEMBLY% %OUTPUTVBCASSEMBLY%"))
                m_Verifications(m_Verifications.Count - 1).Name = "Assembly Comparison Verification"
            End If

            Dim peverify As String
            peverify = PEVerifyPath
            If peverify <> String.Empty AndAlso IO.File.Exists(peverify) Then
                m_Verifications.Add(New ExternalProcessVerification(Me, peverify, "%OUTPUTASSEMBLY% /nologo"))
                m_Verifications(m_Verifications.Count - 1).Name = "Type Safety and Security Verification"
            End If

            If Me.m_Target = "exe" Then
                Dim executor As String
                executor = GetExecutor()
                If executor <> String.Empty AndAlso IO.File.Exists(executor) Then
                    m_Verifications.Add(New ExternalProcessVerification(Me, executor))
                Else
                    m_Verifications.Add(New ExternalProcessVerification(Me, Me.GetOutputAssembly))
                End If
                m_Verifications(m_Verifications.Count - 1).Name = "Output executable verification"
            End If

            If vbccompiler <> String.Empty Then
                Dim bootStrappedExe As String
                bootStrappedExe = IO.Path.Combine(IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(vbccompiler)), Helper.NormalizePath("tests\SelfTest\testoutput\SelfCompile.exe"))

                If IO.File.Exists(bootStrappedExe) AndAlso False Then
                    Dim epv As New ExternalProcessVerification(Me, bootStrappedExe, Join(vbnccmdline, " "))
                    m_Verifications.Add(epv)
                    m_Verifications(m_Verifications.Count - 1).Name = "Bootstrapped verification"
                    epv.Process.WorkingDirectory = m_BasePath
                    epv.Process.UseTemporaryExecutable = True
                    If m_IsNegativeTest Then epv.NegativeError = m_NegativeError
                End If
            End If
        End If

        m_Result = Results.NotRun

        Return True
    End Function

    Sub SaveTest()
        Const DATETIMEFORMAT As String = "yyyy-MM-dd HHmm"
        Dim compiler As String
        Dim filename As String

        Try
            compiler = "(" & VBNCVerification.Process.FileVersion.FileVersion & " " & VBNCVerification.Process.LastWriteDate.ToString(DATETIMEFORMAT) & ")"
            compiler &= "." & m_Result.ToString
            filename = IO.Path.Combine(Me.OutputPath, Me.Name & "." & compiler & ".testresult")
            Using contents As New Xml.XmlTextWriter(filename, Nothing)
                contents.Formatting = Xml.Formatting.Indented
                If False Then
                    Dim ser As New Xml.Serialization.XmlSerializer(GetType(Test))
                    ser.Serialize(contents, Me)
                Else
                    contents.WriteStartDocument(True)
                    contents.WriteStartElement("Test")
                    contents.WriteElementString("Name", Me.Name)
                    contents.WriteStartElement("Date")
                    contents.WriteValue(Me.LastRun)
                    contents.WriteEndElement()
                    contents.WriteElementString("Compiler", compiler)
                    contents.WriteElementString("Result", Me.Result.ToString)
                    contents.WriteElementString("IsNegativeTest", Me.IsNegativeTest.ToString)
                    contents.WriteElementString("NegativeError", Me.NegativeError.ToString)
                    contents.WriteElementString("TestDuration", Me.TestDuration.ToString)

                    contents.WriteStartElement("Verifications")
                    For Each ver As VerificationBase In Me.Verifications
                        contents.WriteStartElement(ver.GetType.Name)
                        contents.WriteElementString("Name", ver.Name)
                        contents.WriteElementString("Result", ver.Result.ToString)
                        contents.WriteElementString("Run", ver.Run.ToString)
                        contents.WriteElementString("NegativeError", ver.NegativeError.ToString)
                        contents.WriteElementString("DescriptiveMessage", ver.DescriptiveMessage)
                        contents.WriteEndElement()
                    Next
                    contents.WriteEndElement()

                    contents.WriteEndElement()
                    contents.WriteEndDocument()
                End If
            End Using
        Catch ex As Exception
            Debug.WriteLine(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Sub DoTest()
        If CreateVerifications() = False Then
            Return
        End If

        RaiseEvent Executing(Me)

        Dim StartTime, EndTime As Date
        StartTime = Date.Now
        For Each v As VerificationBase In m_Verifications
            If v.Verify = False Then
                m_Result = Results.Failed
                Exit For
            End If
        Next
        EndTime = Date.Now
        m_TestDuration = EndTime - StartTime
        m_LastRun = StartTime

        If m_Result = Results.NotRun Then
            m_Result = Results.Success
        End If

        SaveTest()
        m_LoadedOldResults = False
        LoadOldResults()

        RaiseEvent Executed(Me)
    End Sub

    ReadOnly Property VBNCVerification() As ExternalProcessVerification
        Get
            For Each ver As VerificationBase In m_Verifications
                If ver.Name.Contains("VBNC Compile") Then
                    Return DirectCast(ver, ExternalProcessVerification)
                End If
            Next
            Return Nothing
        End Get
    End Property

    ReadOnly Property Message() As String
        Get
            Dim result As String = ""
            For Each v As VerificationBase In m_Verifications
                If v IsNot Nothing Then
                    result &= v.DescriptiveMessage & vbNewLine & New String("*"c, 50) & vbNewLine
                End If
            Next
            Return result
        End Get
    End Property

    Shared Function GetTestName(ByVal Filename As String) As String
        Dim result As String
        result = IO.Path.GetFileNameWithoutExtension(Filename)
        If Filename Like "*.[0-9].vb" Then 'Multi file test.
            result = IO.Path.GetFileNameWithoutExtension(result)
        End If
        Return result
    End Function

    Sub New()

    End Sub

    Sub New(ByVal Path As String, ByVal Parent As Tests)
        m_Parent = Parent
        If Path.EndsWith(IO.Path.DirectorySeparatorChar) Then
            Path = Path.Remove(Path.Length - 1, 1)
        End If

        m_BasePath = IO.Path.GetDirectoryName(Path)
        m_Files.Add(Path)

        m_Name = GetTestName(Path)

        'Test to see if it is a negative test.
        'Negative tests are:
        '0001.vb
        '0001-2.vb
        '0001-3 sometest.vb
        If m_NegativeRegExpTest.IsMatch(m_Name) Then
            Dim tmp As String = m_Name
            If tmp.Contains(" "c) Then tmp = tmp.Substring(0, tmp.IndexOf(" "c))
            If tmp.Contains("-"c) Then tmp = tmp.Substring(0, tmp.IndexOf("-"c))
            m_IsNegativeTest = Integer.TryParse(tmp, m_NegativeError)
        End If
        m_OutputPath = IO.Path.Combine(m_BasePath, DefaultOutputPath)
    End Sub

    Private Function IsNoConfig(ByVal text As String) As Boolean
        Return text.IndexOf("/noconfig", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Function GetTarget(ByVal text As String, ByVal DefaultTarget As String) As String
        If text.IndexOf("/target:exe", StringComparison.OrdinalIgnoreCase) >= 0 OrElse text.IndexOf("/t:exe", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "exe"
        If text.IndexOf("/target:winexe", StringComparison.OrdinalIgnoreCase) >= 0 OrElse text.IndexOf("/t:winexe", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "exe"
        If text.IndexOf("/target:library", StringComparison.OrdinalIgnoreCase) >= 0 OrElse text.IndexOf("/t:library", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "dll"
        If text.IndexOf("/target:module", StringComparison.OrdinalIgnoreCase) >= 0 OrElse text.IndexOf("/t:module", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "netmodule"
        Return DefaultTarget
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
