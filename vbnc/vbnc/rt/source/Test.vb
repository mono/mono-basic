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
    Private m_TargetLocation As String
    Private m_TargetExtension As String
    Private m_NoConfig As Boolean
    private m_References as New Generic.List(of String)

    Private m_LastRun As Date

    Private m_Tag As Object
    Private m_DontExecute As Boolean
    Private m_KnownFailure As Boolean
    Private m_IsNegativeTest As Boolean
    Private m_IsWarning As Boolean
    Private m_NegativeError As Integer
    Private m_LoadedOldResults As Boolean

    Public Event Executed(ByVal Sender As Test)
    Public Event Executing(ByVal Sender As Test)
    Public Event Changed(ByVal Sender As Test)

    Private m_Compiler As String
    Private m_AC As String
    Private Shared m_NegativeRegExpTest As New System.Text.RegularExpressions.Regex("^\d\d\d\d.*$", System.Text.RegularExpressions.RegexOptions.Compiled)
    Private Shared m_FileCache As New Collections.Generic.Dictionary(Of String, String())
    Private Shared m_FileCacheTime As Date = Date.MinValue
    Public Shared DirectoriesToSkip As String()

    Property KnownFailure() As Boolean
        Get
            Return m_KnownFailure
        End Get
        Set(ByVal value As Boolean)
            m_KnownFailure = value
        End Set
    End Property

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
        Dim allfiles() As String

        If IO.Directory.Exists(Me.OutputPath) = False Then
            IO.Directory.CreateDirectory(Me.OutputPath)
        End If

        If m_FileCache.ContainsKey(Me.OutputPath) = False OrElse (Date.Now - m_FileCacheTime).TotalMinutes > 1 Then
            allfiles = IO.Directory.GetFiles(Me.OutputPath, "*.testresult")
            m_FileCache(Me.OutputPath) = allfiles
            m_FileCacheTime = Date.Now
        Else
            allfiles = m_FileCache(Me.OutputPath)
        End If

        Dim files As New Generic.List(Of String)
        Try
            Dim start As String = IO.Path.DirectorySeparatorChar & Me.Name & ".("
            For i As Integer = 0 To allfiles.Length - 1
                If allfiles(i).Contains(start) Then
                    files.Add(allfiles(i))
                End If
            Next
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

    ReadOnly Property Skipped() As Boolean
        Get
            Return m_Result = Results.Skipped
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
        If m_TargetLocation IsNot Nothing Then Return m_TargetLocation
        Return IO.Path.Combine(Me.OutputPath, Name & "." & m_TargetExtension)
    End Function

    Function GetOutputVBCAssembly() As String
        If m_TargetLocation IsNot Nothing Then Return Nothing
        Return IO.Path.Combine(Me.OutputPath, Name & "_vbc." & m_TargetExtension)
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
        Dim outputFilename, outputPath As String
        If ForVBC Then
            outputFilename = GetOutputVBCAssembly()
            If outputFilename Is Nothing Then Return New String() {}
        Else
            outputFilename = GetOutputAssembly()
        End If
        outputPath = IO.Path.GetDirectoryName(outputFilename)
        If outputPath <> "" AndAlso IO.Directory.Exists(outputPath) = False Then
            IO.Directory.CreateDirectory(outputPath)
        End If
        result.Add(String.Format(OutArgument, outputFilename))

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

        rsp = IO.Path.Combine(m_BasePath, Name) & ".response"
        If IO.File.Exists(rsp) Then m_ResponseFile = rsp Else m_ResponseFile = ""
        rsp = IO.Path.Combine(m_BasePath, Name) & ".rsp"
        If IO.File.Exists(rsp) Then m_RspFile = rsp Else m_RspFile = ""
        rsp = IO.Path.Combine(m_BasePath, "all.rsp")
        If IO.File.Exists(rsp) Then m_DefaultRspFile = rsp Else m_DefaultRspFile = ""

        'Find the target of the test (exe, winexe, library, module)
        m_Target = "exe" 'default target.
        If m_RspFile <> "" Then
            ParseResponseFile(m_RspFile)
        Else
            If m_DefaultRspFile <> "" Then
                ParseResponseFile(m_DefaultRspFile)
            End If
            If m_ResponseFile <> "" Then
                ParseResponseFile(m_ResponseFile)
            End If
        End If
        m_TargetExtension = GetTargetExtension(m_Target)
    End Sub

    Sub ParseResponseFile(ByVal Filename As String)
        Dim contents As String()

        If IO.File.Exists(Filename) = False Then Return

        contents = IO.File.ReadAllLines(Filename)

        For Each line As String In contents
            If line.Contains("#DONTEXECUTE#") Then m_DontExecute = True

            For Each arg As String In Helper.ParseLine(line)
                If arg.StartsWith("@") Then
                    ParseResponseFile(line.Substring(1))
                    Continue For
                ElseIf Not (line.StartsWith("-"c) OrElse line.StartsWith("/"c)) Then
                    Continue For
                End If

                Dim name, value As String
                Dim iStart As Integer

                iStart = arg.IndexOfAny(New Char() {":"c, "="c})

                If iStart >= 0 Then
                    name = arg.Substring(1, iStart - 1)
                    value = arg.Substring(iStart + 1)
                Else
                    name = arg.Substring(1)
                    value = arg.Substring(1)
                End If

                Select Case name.ToUpperInvariant()
                    Case "OUT"
                        m_TargetLocation = value
                        m_TargetLocation = IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(Filename), m_TargetLocation))
                    Case "R", "REFERENCE"
                        If value.IndexOfAny(New Char() {":"c, "/"c, "\"c, ":"c}) >= 0 Then
                            Dim ref As String
                            ref = IO.Path.GetFullPath(value)
                            If m_References.Contains(ref) = False Then
                                m_References.Add(ref)
                            End If
                        End If
                    Case "TARGET", "T"
                        m_Target = GetTarget(arg, m_Target)
                    Case "NOCONFIG"
                        m_NoConfig = True
                End Select
            Next
        Next
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
            If Me.GetOutputVBCAssembly Is Nothing Then Return False
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
            Return IO.Path.GetFullPath("..\..\..\ac\bin\ac.exe".Replace("\", IO.Path.DirectorySeparatorChar))
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
        If vbccompiler <> String.Empty AndAlso vbccmdline.Length > 0 Then
            vbc = New ExternalProcessVerification(Me, vbccompiler, Join(vbccmdline, " "))
            vbc.Process.WorkingDirectory = m_BasePath
            vbc.Name = "VBC Compile (verifies that the test itself is correct)"
            If m_IsNegativeTest Then vbc.NegativeError = m_NegativeError
            If m_IsWarning Then vbc.Warning = m_NegativeError
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
        If m_IsWarning Then m_Compilation.Warning = m_NegativeError

        m_Verifications.Clear()

        If vbc IsNot Nothing AndAlso IsSourceDirty Then m_Verifications.Add(vbc)
        m_Verifications.Add(m_Compilation)

        If m_IsNegativeTest = False Then
            If vbccompiler <> String.Empty AndAlso Me.m_Target = "exe" AndAlso m_DontExecute = False AndAlso Me.GetOutputVBCAssembly IsNot Nothing Then
                m_Verifications.Add(New ExternalProcessVerification(Me, Me.GetOutputVBCAssembly))
                m_Verifications(m_Verifications.Count - 1).Name = "Test executable verification"
            End If

            Dim peverify As String
            peverify = Environment.ExpandEnvironmentVariables(PEVerifyPath)
            If peverify <> String.Empty AndAlso IO.File.Exists(peverify) Then
                Dim peV As New ExternalProcessVerification(Me, peverify, "%OUTPUTASSEMBLY% /nologo /verbose")
                peV.Name = "Type Safety and Security Verification"
                peV.Process.DependentFiles.AddRange(m_References)
                peV.Process.WorkingDirectory = IO.Path.Combine(BasePath, "testoutput")
                m_Verifications.Add(peV)
            End If

            Dim ac As String
            ac = GetACPath
            If ac <> String.Empty AndAlso vbccompiler <> String.Empty AndAlso IO.File.Exists(ac) AndAlso IO.File.Exists(vbccompiler) AndAlso Me.GetOutputVBCAssembly IsNot Nothing Then
                Dim cmdLine As String = "%OUTPUTASSEMBLY% %OUTPUTVBCASSEMBLY%"
                Dim acV As New ExternalProcessVerification(Me, GetACPath, cmdLine)
                acV.Name = "Assembly Comparison Verification"
                acV.Process.DependentFiles.AddRange(m_References)
                acV.Process.WorkingDirectory = IO.Path.Combine(BasePath, "testoutput")
                m_Verifications.Add(acV)
            End If

            If Me.m_Target = "exe" AndAlso m_DontExecute = False Then
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
	
            Dim i As Integer
            i = compiler.IndexOfAny (IO.Path.GetInvalidPathChars)
            If i >= 0 Then
                For Each c As Char In IO.Path.GetInvalidPathChars
                    compiler = compiler.Replace (c.ToString (), "")
                Next
            End If

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
            Console.WriteLine(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Function SkipTest() As Boolean
        If SkipTest(directoriestoskip) Then Return True
        If Helper.IsOnWindows Then
            Return Name.EndsWith(".Linux", StringComparison.OrdinalIgnoreCase)
        Else
            Return Name.EndsWith(".Windows", StringComparison.OrdinalIgnoreCase)
        End If
    End Function

    Private Function SkipTest(ByVal DirectoriesToSkip As String()) As Boolean
        If DirectoriesToSkip Is Nothing Then Return False

        For i As Integer = 0 To DirectoriesToSkip.Length - 1
            If m_BasePath.Contains(DirectoriesToSkip(i)) Then
                Return True
            End If
        Next

        Return False
    End Function

    Sub DoTest()
        If BasePath <> "" Then
            Environment.CurrentDirectory = BasePath
        End If
        If CreateVerifications() = False Then
            Return
        End If

        m_Result = Results.Running
        RaiseEvent Executing(Me)

        Dim StartTime, EndTime As Date
        StartTime = Date.Now
        If SkipTest() Then
            m_Result = Results.Skipped
        Else
            For i As Integer = 0 To m_Verifications.Count - 1
                Dim v As VerificationBase = m_Verifications(i)
                If v.Verify = False Then
                    m_Result = Results.Failed
                    Exit For
                End If
            Next
        End If
        EndTime = Date.Now
        m_TestDuration = EndTime - StartTime
        m_LastRun = StartTime

        If m_Result = Results.Running Then
            m_Result = Results.Success
        End If
        If m_KnownFailure Then
            If m_Result = Results.Success Then
                m_Result = Results.KnownFailureSucceeded
            ElseIf m_Result = Results.Failed Then
                m_Result = Results.KnownFailureFailed
            Else
                m_Result = Results.KnownFailureFailed
            End If
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
            Dim firstNonNumber As Integer = m_Name.Length
            For i As Integer = 0 To m_Name.Length - 1
                If Char.IsNumber(m_Name(i)) = False Then
                    firstNonNumber = i
                    Exit For
                End If
            Next
            m_IsNegativeTest = Integer.TryParse(m_Name.Substring(0, firstNonNumber), m_NegativeError)
            If m_IsNegativeTest AndAlso m_NegativeError >= 40000 AndAlso m_NegativeError < 50000 Then
                m_IsNegativeTest = False
                m_IsWarning = True
            End If
        End If
        m_OutputPath = IO.Path.Combine(m_BasePath, DefaultOutputPath)
    End Sub

    Private Function IsNoConfig(ByVal text As String) As Boolean
        Return text.IndexOf("/noconfig", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Function GetTarget(ByVal text As String, ByVal DefaultTarget As String) As String
        Dim prefixes As String() = New String() {"/target:", "/t:", "-target:", "-t:"}
        For Each prefix As String In prefixes
            If text.IndexOf(prefix & "exe", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "exe"
            If text.IndexOf(prefix & "winexe", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "winexe"
            If text.IndexOf(prefix & "library", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "dll"
            If text.IndexOf(prefix & "module", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "netmodule"
        Next
        Return DefaultTarget
    End Function

    Private Function GetTargetExtension(ByVal Target As String) As String
        Select Case Target
            Case "winexe", "exe"
                Return "exe"
            Case "library", "dll"
                Return "dll"
            Case "netmodule", "module"
                Return "netmodule"
            Case Else
                Return "exe"
        End Select
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
