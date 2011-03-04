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

<Serializable()> _
Public Class Test
    Private ReadOnly PEVerifyPath As String = System.Environment.ExpandEnvironmentVariables("%programfiles%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\PEVerify.exe")
    Private ReadOnly PEVerifyPath2 As String = System.Environment.ExpandEnvironmentVariables("%programfiles%\Microsoft SDKs\\Windows\v6.0A\bin\PEVerify.exe")
    Private ReadOnly PEVerifyPath3 As String = System.Environment.ExpandEnvironmentVariables("%programfiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\PEVerify.exe")

    ''' <summary>
    ''' The id of the test
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ID As String

    ''' <summary>
    ''' The name of the test
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Name As String

    Private m_Arguments As String
    ''' <summary>
    ''' Arguments specific to vbc (such as compile into a different location). Will be appended after the rest of the arguments.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_VBCArguments As String
    Private m_KnownFailure As String

    ''' <summary>
    ''' The target (winexe, exe, library, module)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Target As Targets

    ''' <summary>
    ''' The files that contains this test.
    ''' </summary>
    Private m_Files As New Specialized.StringCollection

    ''' <summary>
    ''' A list of files this test depends on to be in the current directory (and the output directory)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Dependencies As New Specialized.StringCollection

    ''' <summary>
    ''' The exit code the compiler should return
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ExpectedExitCode As Integer
    Private m_ExpectedVBCExitCode As Nullable(Of Integer)

    ''' <summary>
    ''' The result of the test
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Result As Results

    ''' <summary>
    ''' The test container
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Parent As Tests

    ''' <summary>
    ''' How long did the test take?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TestDuration As TimeSpan

    Private m_Verifications As New Generic.List(Of VerificationBase)

    Private m_WorkingDirectory As String
    Private m_OutputAssembly As String
    Private m_OutputVBCAssembly As String

    ''' <summary>
    ''' The compilation using our compiler.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Compilation As VerificationBase

    Private m_LastRun As Date

    Private m_Tag As Object
    Private m_DontExecute As Boolean
    Private m_MyType As MyTypes
    ''' <summary>
    ''' The arguments to pass to the compiled executable (if it is an executable) to test it.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TestArguments As String
    Private m_Errors As New Generic.List(Of ErrorInfo)
    Private m_VBCErrors As Generic.List(Of ErrorInfo)
    Private m_Description As String

    Public Event Executed(ByVal Sender As Test)
    Public Event Executing(ByVal Sender As Test)
    Public Event Changed(ByVal Sender As Test)

    Private m_Compiler As String

    Public Property Description As String
        Get
            Return m_Description
        End Get
        Set(ByVal value As String)
            m_Description = value
        End Set
    End Property

    Public ReadOnly Property Errors As Generic.List(Of ErrorInfo)
        Get
            Return m_Errors
        End Get
    End Property

    Public Property VBCErrors As Generic.List(Of ErrorInfo)
        Get
            Return m_VBCErrors
        End Get
        Set(ByVal value As Generic.List(Of ErrorInfo))
            m_VBCErrors = value
        End Set
    End Property

    Public Property MyType As MyTypes
        Get
            Return m_MyType
        End Get
        Set(ByVal value As MyTypes)
            m_MyType = value
        End Set
    End Property

    Public Property TestArguments() As String
        Get
            Return m_TestArguments
        End Get
        Set(ByVal value As String)
            m_TestArguments = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return m_ID
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(m_ID) Then
                Throw New ArgumentException("This test already has an ID")
            End If

            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentException("Invalid ID")
            End If

            m_ID = value
        End Set
    End Property

    Private Function GetAttributeValue(ByVal attrib As XmlAttribute) As String
        If attrib Is Nothing Then Return Nothing
        Return attrib.Value
    End Function

    Private Function GetAttributeIntValue(ByVal attrib As XmlAttribute) As Integer
        If attrib Is Nothing Then Return Nothing
        Return Integer.Parse(attrib.Value)
    End Function

    Private Function GetNodeValue(ByVal node As XmlNode) As String
        If node Is Nothing Then Return Nothing
        Return node.InnerText
    End Function

    Public Sub SetResult(ByVal result As String)
        If result Is Nothing Then result = String.Empty
        Select Case result.ToLower()
            Case Nothing, "", "notrun"
                m_Result = Results.NotRun
            Case Else
                m_Result = CType([Enum].Parse(GetType(Results), result, True), Results)
        End Select
    End Sub

    Public Sub LoadResult(ByVal xml As XmlNode)
        Dim result As String = GetAttributeValue(xml.Attributes("result"))
        For Each vb As XmlNode In xml.SelectNodes("Verification")
            Dim type As String = GetAttributeValue(vb.Attributes("Type"))
            Dim name As String = GetAttributeValue(vb.Attributes("Name"))
            Dim executable As String
            Dim expandablecommandline As String
            Dim verification As VerificationBase
            Select Case type
                Case "rt.ExternalProcessVerification"
                    Dim extvb As ExternalProcessVerification
                    Dim process As XmlNode = vb.SelectSingleNode("Process")
                    executable = GetAttributeValue(process.Attributes("Executable"))
                    expandablecommandline = GetAttributeValue(process.Attributes("UnexpandedCommandLine"))
                    extvb = New ExternalProcessVerification(Me, executable, expandablecommandline)
                    extvb.Process.StdOut = process.SelectSingleNode("StdOut").Value
                    verification = extvb
                Case "rt.CecilCompare"
                    Dim cecilvb As CecilCompare
                    cecilvb = New CecilCompare(Me)
                    verification = cecilvb
                Case Else
                    Throw New NotImplementedException(type)
            End Select
            verification.DescriptiveMessage = GetAttributeValue(vb.Attributes("DescriptiveMessage"))
            verification.Name = name
            verification.Result = CBool(GetAttributeValue(vb.Attributes("Result")))
            verification.Run = CBool(GetAttributeValue(vb.Attributes("Run")))
            Me.m_Verifications.Add(verification)
        Next
        SetResult(result)
    End Sub

    Public Sub Load(ByVal xml As XmlNode)
        Dim target As String
        Dim mytype As String
        Dim tmp As String

        m_ID = xml.Attributes("id").Value
        m_Name = xml.Attributes("name").Value
        m_Arguments = GetNodeValue(xml.SelectSingleNode("arguments"))
        m_VBCArguments = GetNodeValue(xml.SelectSingleNode("vbcarguments"))
        m_TestArguments = GetNodeValue(xml.SelectSingleNode("testarguments"))
        m_KnownFailure = GetAttributeValue(xml.Attributes("knownfailure"))
        m_OutputVBCAssembly = GetFullPath(GetAttributeValue(xml.Attributes("outputvbcassembly")))
        m_OutputAssembly = GetFullPath(GetAttributeValue(xml.Attributes("outputassembly")))

        m_ExpectedExitCode = CInt(GetAttributeValue(xml.Attributes("expectedexitcode")))
        tmp = GetAttributeValue(xml.Attributes("expectedvbcerrorcode"))
        tmp = GetAttributeValue(xml.Attributes("expectedvbcexitcode"))
        If Not String.IsNullOrEmpty(tmp) Then m_ExpectedVBCExitCode = CInt(tmp)
        m_WorkingDirectory = GetAttributeValue(xml.Attributes("workingdirectory"))

        SetResult(GetAttributeValue(xml.Attributes("result")))

        target = GetAttributeValue(xml.Attributes("target"))
        If target IsNot Nothing Then target = target.ToLower()
        Select Case target
            Case "exe"
                m_Target = Targets.Exe
            Case "winexe"
                m_Target = Targets.Winexe
            Case "library"
                m_Target = Targets.Library
            Case "module"
                m_Target = Targets.Module
            Case "", Nothing
                m_Target = Targets.Library
            Case "none"
                m_Target = Targets.None
            Case Else
                Throw New InvalidOperationException("Invalid target: " & target)
        End Select

        mytype = GetAttributeValue(xml.Attributes("mytype"))
        If mytype IsNot Nothing Then mytype = mytype.ToLower()
        Select Case mytype
            Case "console"
                m_MyType = MyTypes.Console
            Case "custom"
                m_MyType = MyTypes.Custom
            Case "empty"
                m_MyType = MyTypes.Empty
            Case "web"
                m_MyType = MyTypes.Web
            Case "webcontrol"
                m_MyType = MyTypes.WebControl
            Case "windows"
                m_MyType = MyTypes.Windows
            Case "windowsforms"
                m_MyType = MyTypes.WindowsForms
            Case "windowsformswithcustomsubmain"
                m_MyType = MyTypes.WindowsFormsWithCustomSubMain
            Case "", Nothing
                m_MyType = MyTypes.Default
            Case Else
                Throw New InvalidOperationException("Invalid mytype: " & mytype)
        End Select

        For Each file As XmlNode In xml.SelectNodes("file")
            m_Files.Add(file.InnerText)
        Next
        For Each file As XmlNode In xml.SelectNodes("dependency")
            m_Dependencies.Add(file.InnerText)
        Next

        m_Description = GetNodeValue(xml.SelectSingleNode("description"))

        For Each e As XmlNode In xml.SelectNodes("error")
            m_Errors.Add(New ErrorInfo(GetAttributeIntValue(e.Attributes("line")), GetAttributeIntValue(e.Attributes("number")), GetAttributeValue(e.Attributes("message"))))
        Next
        For Each e As XmlNode In xml.SelectNodes("vbcerror")
            If m_VBCErrors Is Nothing Then m_VBCErrors = New Generic.List(Of ErrorInfo)
            m_VBCErrors.Add(New ErrorInfo(GetAttributeIntValue(e.Attributes("line")), GetAttributeIntValue(e.Attributes("number")), GetAttributeValue(e.Attributes("message"))))
        Next
    End Sub

    Private Function GetFullPath(ByVal path As String) As String
        If String.IsNullOrEmpty(path) Then Return path
        If IO.Path.IsPathRooted(path) Then Return path
        Return IO.Path.Combine(IO.Path.GetDirectoryName(Parent.Filename), path)
    End Function

    Private Function GetRelativePath(ByVal path As String) As String
        Dim dir As String = IO.Path.GetDirectoryName(Parent.Filename)
        Dim result As String

        If Not path.StartsWith(dir) Then Return path

        result = path.Substring(dir.Length)
        If result.StartsWith(IO.Path.DirectorySeparatorChar) Then
            result = result.Substring(1)
        End If

        Return result
    End Function

    Public Sub Save(ByVal xml As Xml.XmlWriter, ByVal results As Boolean)
        xml.WriteStartElement("test")
        xml.WriteAttributeString("id", m_ID)
        If results = False Then
            xml.WriteAttributeString("name", m_Name)
            If Not String.IsNullOrEmpty(m_KnownFailure) Then
                xml.WriteAttributeString("knownfailure", m_KnownFailure)
            End If
            If Not String.IsNullOrEmpty(m_OutputVBCAssembly) Then
                xml.WriteAttributeString("outputvbcassembly", GetRelativePath(m_OutputVBCAssembly))
            End If
            If Not String.IsNullOrEmpty(m_OutputAssembly) Then
                xml.WriteAttributeString("outputassembly", GetRelativePath(m_OutputAssembly))
            End If

            If m_ExpectedExitCode <> 0 Then xml.WriteAttributeString("expectedexitcode", m_ExpectedExitCode.ToString())
            'If m_ExpectedErrorCode <> 0 Then xml.WriteAttributeString("expectederrorcode", m_ExpectedErrorCode.ToString())
            If m_ExpectedVBCExitCode.HasValue Then xml.WriteAttributeString("expectedvbcexitcode", m_ExpectedVBCExitCode.Value.ToString())
            'If m_ExpectedVBCErrorCode.HasValue Then xml.WriteAttributeString("expectedvbcerrorcode", m_ExpectedVBCErrorCode.Value.ToString())

            If m_Target <> Targets.Library Then xml.WriteAttributeString("target", m_Target.ToString().ToLower())
            If m_MyType <> MyTypes.Default Then xml.WriteAttributeString("mytype", m_MyType.ToString().ToLower())

            If Not String.IsNullOrEmpty(m_WorkingDirectory) Then xml.WriteAttributeString("workingdirectory", m_WorkingDirectory)
            If Not String.IsNullOrEmpty(m_Arguments) Then xml.WriteElementString("arguments", m_Arguments)
            If Not String.IsNullOrEmpty(m_VBCArguments) Then xml.WriteElementString("vbcarguments", m_VBCArguments)
            If Not String.IsNullOrEmpty(m_TestArguments) Then xml.WriteElementString("testarguments", m_TestArguments)
            For Each file As String In m_Files
                If IO.Path.IsPathRooted(file) AndAlso file.StartsWith(IO.Path.GetDirectoryName(Parent.Filename)) Then
                    file = file.Substring(IO.Path.GetDirectoryName(Parent.Filename).Length)
                    If file.StartsWith(IO.Path.DirectorySeparatorChar) Then file = file.Substring(1)
                End If
                xml.WriteElementString("file", file)
            Next
            For Each file As String In m_Dependencies
                xml.WriteElementString("dependency", file)
            Next
            If Not String.IsNullOrEmpty(m_Description) Then xml.WriteElementString("description", m_Description)
            For Each Err As ErrorInfo In m_Errors
                xml.WriteStartElement("error")
                If Err.Line <> 0 Then xml.WriteAttributeString("line", Err.Line.ToString())
                If Err.Number <> 0 Then xml.WriteAttributeString("number", Err.Number.ToString())
                If String.IsNullOrEmpty(Err.Message) = False Then xml.WriteAttributeString("message", Err.Message)
                xml.WriteEndElement()
            Next
            If m_VBCErrors IsNot Nothing Then
                For Each Err As ErrorInfo In m_VBCErrors
                    xml.WriteStartElement("vbcerror")
                    If Err.Line <> 0 Then xml.WriteAttributeString("line", Err.Line.ToString())
                    If Err.Number <> 0 Then xml.WriteAttributeString("number", Err.Number.ToString())
                    If String.IsNullOrEmpty(Err.Message) = False Then xml.WriteAttributeString("message", Err.Message)
                    xml.WriteEndElement()
                Next
            End If
        Else
            xml.WriteAttributeString("result", m_Result.ToString().ToLower())
            For Each vb As VerificationBase In Me.Verifications
                xml.WriteStartElement("Verification")
                xml.WriteAttributeString("Type", vb.GetType().FullName)
                xml.WriteAttributeString("Name", vb.Name)
                xml.WriteAttributeString("DescriptiveMessage", vb.DescriptiveMessage)
                xml.WriteAttributeString("ExpectedExitCode", vb.ExpectedExitCode.ToString())
                xml.WriteAttributeString("Result", vb.Result.ToString())
                xml.WriteAttributeString("Run", vb.Run.ToString())

                Dim extvb As ExternalProcessVerification = TryCast(vb, ExternalProcessVerification)
                If extvb IsNot Nothing Then
                    If extvb.Process IsNot Nothing Then
                        xml.WriteStartElement("Process")
                        xml.WriteAttributeString("Executable", extvb.Process.Executable)
                        xml.WriteAttributeString("UnexpandedCommandLine", extvb.Process.UnexpandedCommandLine)
                        xml.WriteElementString("StdOut", extvb.Process.StdOut)
                        xml.WriteEndElement()
                    End If
                End If

                xml.WriteEndElement()
            Next
        End If
        xml.WriteEndElement()
    End Sub

    Property ExpectedExitCode() As Integer
        Get
            Return m_ExpectedExitCode
        End Get
        Set(ByVal value As Integer)
            m_ExpectedExitCode = value
        End Set
    End Property

    Property ExpectedVBCExitCode() As Nullable(Of Integer)
        Get
            Return m_ExpectedVBCExitCode
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ExpectedVBCExitCode = value
        End Set
    End Property

    Public Property KnownFailure() As String
        Get
            Return m_KnownFailure
        End Get
        Set(ByVal value As String)
            m_KnownFailure = value
        End Set
    End Property

    ReadOnly Property IsKnownFailure() As Boolean
        Get
            Return String.IsNullOrEmpty(m_KnownFailure) = False
        End Get
    End Property

    Property Compiler() As String
        Get
            Return m_Compiler
        End Get
        Set(ByVal value As String)
            m_Compiler = value
        End Set
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

    ReadOnly Property TestDuration() As TimeSpan
        Get
            Return m_TestDuration
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
    ''' The path of where the output files are
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property OutputPath() As String
        Get
            Return IO.Path.Combine(FullWorkingDirectory, "testoutput")
        End Get
    End Property

    ''' <summary>
    ''' The files that contains this test.
    ''' </summary>
    <System.ComponentModel.Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
    ReadOnly Property Files() As Specialized.StringCollection
        Get
            Return m_Files
        End Get
    End Property

    ''' <summary>
    ''' The dependencies of this test.
    ''' </summary>
    <System.ComponentModel.Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
    ReadOnly Property Dependencies() As Specialized.StringCollection
        Get
            Return m_Dependencies
        End Get
    End Property

    ''' <summary>
    ''' The StdOut of the test
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property StdOut() As String
        Get
            Dim external As ExternalProcessVerification = TryCast(m_Compilation, ExternalProcessVerification)
            If external IsNot Nothing Then
                If external.Process IsNot Nothing Then Return external.Process.StdOut
            End If

            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' The exit code of the compilation
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ExitCode() As Integer
        Get
            Dim external As ExternalProcessVerification = TryCast(m_Compilation, ExternalProcessVerification)

            If external IsNot Nothing Then
                If external.Process Is Nothing Then Return 0
                Return external.Process.ExitCode
            Else
                Return 0
            End If
        End Get
    End Property

    ''' <summary>
    ''' The name of the test.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
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

    Property Target() As Targets
        Get
            Return m_Target
        End Get
        Set(ByVal value As Targets)
            m_Target = value
        End Set
    End Property

    ReadOnly Property TargetExtension() As String
        Get
            Select Case m_Target
                Case Targets.Exe, Targets.Winexe
                    Return "exe"
                Case Targets.Module
                    Return "netmodule"
                Case Targets.Library
                    Return "dll"
                Case Else
                    Throw New InvalidOperationException("Invalid target: " & m_Target.ToString())
            End Select
        End Get
    End Property

    ReadOnly Property OutputVBCAssemblyFull() As String
        Get
            Dim result As String = OutputVBCAssembly
            If IO.Path.IsPathRooted(result) Then Return result
            Return IO.Path.Combine(FullWorkingDirectory, result)
        End Get
    End Property

    ReadOnly Property OutputAssemblyFull() As String
        Get
            Dim result As String = OutputAssembly
            If IO.Path.IsPathRooted(result) Then Return result
            Return IO.Path.Combine(FullWorkingDirectory, result)
        End Get
    End Property

    Property OutputVBCAssembly() As String
        Get
            If String.IsNullOrEmpty(m_OutputVBCAssembly) Then
                Return IO.Path.Combine(Me.OutputPath, Name & "_vbc." & TargetExtension)
            End If
            Return m_OutputVBCAssembly
        End Get
        Set(ByVal value As String)
            m_OutputVBCAssembly = value
        End Set
    End Property


    Property OutputAssembly() As String
        Get
            If String.IsNullOrEmpty(m_OutputAssembly) Then
                Return IO.Path.Combine(Me.OutputPath, Name & "." & TargetExtension)
            End If
            Return m_OutputAssembly
        End Get
        Set(ByVal value As String)
            m_OutputAssembly = value
        End Set
    End Property

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
            result = New String() {} 'IO.Directory.GetFiles(OutputPath, Name & VerifiedPattern)
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
    Function GetTestCommandLineArguments(Optional ByVal ForVBC As Boolean = False, Optional ByVal IncludeFiles As Boolean = True) As String()
        Dim result As New Generic.List(Of String)

        'Initialize()

        'First option is always the /out: argument.
        Const OutArgument As String = "-out:{0}"
        Dim outputFilename, outputPath As String
        If ForVBC Then
            outputFilename = OutputVBCAssembly()
            If outputFilename Is Nothing Then Return New String() {}
        Else
            outputFilename = OutputAssembly()
        End If
        outputPath = IO.Path.GetDirectoryName(outputFilename)
        If outputPath <> "" AndAlso IO.Directory.Exists(outputPath) = False Then
            IO.Directory.CreateDirectory(outputPath)
        End If
        result.Add(String.Format(OutArgument, outputFilename))

        If m_Arguments IsNot Nothing Then
            result.AddRange(m_Arguments.Split(New Char() {" "c, Chr(10), Chr(13)}, StringSplitOptions.RemoveEmptyEntries))
        End If

        If IncludeFiles Then
            For Each file As String In Files
                result.Add(file.Replace("\"c, IO.Path.DirectorySeparatorChar))
            Next
        End If

        Select Case m_Target
            Case Targets.Library
                result.Add("/target:library")
            Case Targets.Exe
                result.Add("/target:exe")
            Case Targets.Winexe
                result.Add("/target:winexe")
            Case Targets.Module
                result.Add("/target:module")
        End Select

        If m_MyType <> MyTypes.Default Then result.Add("/define:_MYTYPE=\""" & m_MyType.ToString & "\""")

        If ForVBC AndAlso Not String.IsNullOrEmpty(m_VBCArguments) Then
            result.Add(m_VBCArguments)
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

    Property Arguments() As String
        Get
            Return m_Arguments
        End Get
        Set(ByVal value As String)
            m_Arguments = value
        End Set
    End Property

    Property VBCArguments() As String
        Get
            Return m_VBCArguments
        End Get
        Set(ByVal value As String)
            m_VBCArguments = value
        End Set
    End Property

    Property WorkingDirectory() As String
        Get
            Return m_WorkingDirectory
        End Get
        Set(ByVal value As String)
            m_WorkingDirectory = value
        End Set
    End Property

    ReadOnly Property FullWorkingDirectory() As String
        Get
            If String.IsNullOrEmpty(m_WorkingDirectory) Then
                Return IO.Path.GetDirectoryName(m_Parent.Filename)
            Else
                Return IO.Path.Combine(IO.Path.GetDirectoryName(m_Parent.Filename), m_WorkingDirectory)
            End If
        End Get
    End Property

    ReadOnly Property VBCVerification As ExternalProcessVerification
        Get
            For Each v As VerificationBase In m_Verifications
                Dim epv As ExternalProcessVerification = TryCast(v, ExternalProcessVerification)
                If epv Is Nothing Then Continue For
                If epv.Name.StartsWith("VBC ") Then Return epv
            Next
            Return Nothing
        End Get
    End Property

    ReadOnly Property VBNCVerification As ExternalProcessVerification
        Get
            For Each v As VerificationBase In m_Verifications
                Dim epv As ExternalProcessVerification = TryCast(v, ExternalProcessVerification)
                If epv Is Nothing Then Continue For
                If epv.Name.StartsWith("VBNC ") Then Return epv
            Next
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Returns true if new verifications have been created (only if source files has changed
    ''' or vbnc compiler has changed since last run).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateVerifications() As Boolean
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
            vbc.Process.WorkingDirectory = FullWorkingDirectory
        End If

        If vbc IsNot Nothing Then
            vbc.Name = "VBC Compile (verifies that the test itself is correct)"
            If m_ExpectedVBCExitCode.HasValue Then
                vbc.ExpectedExitCode = m_ExpectedVBCExitCode.Value
            Else
                vbc.ExpectedExitCode = m_ExpectedExitCode
            End If
            If m_VBCErrors IsNot Nothing Then
                vbc.ExpectedErrors = m_VBCErrors
            Else
                vbc.ExpectedErrors = m_Errors
            End If
        End If

        compiler = Me.Compiler
        If compiler Is Nothing AndAlso Me.Parent IsNot Nothing Then
            compiler = Me.Parent.VBNCPath
        End If
        If compiler Is Nothing Then
            Throw New Exception("No compiler specified.")
        End If

        Dim external_compilation As ExternalProcessVerification = Nothing

        external_compilation = New ExternalProcessVerification(Me, compiler, Join(vbnccmdline, " "))
        external_compilation.Process.WorkingDirectory = FullWorkingDirectory
        m_Compilation = external_compilation

        m_Compilation.Name = "VBNC Compile"
        external_compilation.Process.UseTemporaryExecutable = True
        m_Compilation.ExpectedErrors = m_Errors
        m_Compilation.ExpectedExitCode = m_ExpectedExitCode

        m_Verifications.Clear()

        If vbc IsNot Nothing Then m_Verifications.Add(vbc)
        m_Verifications.Add(m_Compilation)

        Dim isVbcSuccess As Boolean
        Dim isVbncSuccess As Boolean
        If m_ExpectedVBCExitCode.HasValue Then
            isVbcSuccess = m_ExpectedVBCExitCode.Value = 0
        Else
            isVbcSuccess = m_ExpectedExitCode = 0
        End If
        isVbncSuccess = m_ExpectedExitCode = 0

        If isVbcSuccess Then
            If vbccompiler <> String.Empty AndAlso Me.m_Target = Targets.Exe AndAlso m_DontExecute = False AndAlso Me.OutputVBCAssembly IsNot Nothing Then
                Dim testOutputVerification As ExternalProcessVerification
                testOutputVerification = New ExternalProcessVerification(Me, Me.OutputVBCAssemblyFull, Me.TestArguments)
                testOutputVerification.Name = "VBC compiled executable verification"
                testOutputVerification.Process.WorkingDirectory = Me.FullWorkingDirectory
                m_Verifications.Add(testOutputVerification)
            End If
        End If

        If m_ExpectedExitCode = 0 Then
            Dim peverify As String
            If Helper.IsOnMono Then
                peverify = "peverify"
            Else
                peverify = Environment.ExpandEnvironmentVariables(PEVerifyPath)
                If peverify = String.Empty OrElse IO.File.Exists(peverify) = False Then peverify = Environment.ExpandEnvironmentVariables(PEVerifyPath2)
                If peverify = String.Empty OrElse IO.File.Exists(peverify) = False Then peverify = Environment.ExpandEnvironmentVariables(PEVerifyPath3)
            End If
            If peverify <> String.Empty AndAlso (Helper.IsOnMono OrElse IO.File.Exists(peverify)) Then
                Dim peV As ExternalProcessVerification
                If Helper.IsOnMono Then
                    peV = New ExternalProcessVerification(Me, peverify, "--verify all """ & OutputAssemblyFull & """")
                Else
                    peV = New ExternalProcessVerification(Me, peverify, """" & OutputAssemblyFull & """ /nologo /verbose")
                End If
                peV.Name = "Type Safety and Security Verification"
                peV.Process.WorkingDirectory = Me.OutputPath
                m_Verifications.Add(peV)
            End If
        End If

        If isVbcSuccess AndAlso isVbncSuccess Then
            Dim cc As CecilCompare
            If vbccompiler <> String.Empty AndAlso IO.File.Exists(vbccompiler) AndAlso OutputVBCAssembly() IsNot Nothing Then
                cc = New CecilCompare(Me)
                cc.Name = "Cecil Assembly Compare"
                m_Verifications.Add(cc)
            End If
        End If

        If m_ExpectedExitCode = 0 Then
            If Me.m_Target = Targets.Exe AndAlso m_DontExecute = False Then
                m_Verifications.Add(New ExternalProcessVerification(Me, Me.OutputAssemblyFull, Me.TestArguments))
                m_Verifications(m_Verifications.Count - 1).Name = "VBNC compiled executable verification"
            End If
        End If

        m_Result = Results.NotRun

        Return True
    End Function

    Function SkipTest() As Boolean
        If Helper.IsOnWindows Then
            Return Name.EndsWith(".Linux", StringComparison.OrdinalIgnoreCase)
        Else
            Return Name.EndsWith(".Windows", StringComparison.OrdinalIgnoreCase)
        End If
    End Function

    Sub DoTest()
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
            Try
                'Copy dependencies
                For Each file As String In m_Dependencies
                    Dim src As String = IO.Path.Combine(FullWorkingDirectory, file)
                    Dim dst As String = IO.Path.Combine(FullWorkingDirectory, IO.Path.GetFileName(src))
                    IO.File.Copy(src, dst, True)
                    dst = IO.Path.Combine(IO.Path.GetDirectoryName(OutputAssemblyFull), IO.Path.GetFileName(src))
                    If dst <> src Then IO.File.Copy(src, dst, True)
                Next
            Catch
                m_Result = Results.Failed
            End Try

            'Run test
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
        If IsKnownFailure Then
            If m_Result = Results.Success Then
                m_Result = Results.KnownFailureSucceeded
            ElseIf m_Result = Results.Failed Then
                m_Result = Results.KnownFailureFailed
            Else
                m_Result = Results.KnownFailureFailed
            End If
        End If

        RaiseEvent Executed(Me)
    End Sub

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

    Sub New(ByVal Parent As Tests)
        m_Parent = Parent
    End Sub

    Sub New(ByVal Parent As Tests, ByVal xml As XmlNode)
        m_Parent = Parent
        Load(xml)
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
End Class

