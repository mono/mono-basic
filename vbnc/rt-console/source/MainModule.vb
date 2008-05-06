Imports System.Reflection

Module MainModule

    Function Main(ByVal args() As String) As Integer
        Try
            Return Execute(args)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
            Return 1
        End Try
    End Function

    Function Execute(ByVal args() As String) As Integer
        Dim Type As Type
        Dim rt As New rt_console
        Dim Properties As PropertyInfo()

        Type = rt.GetType
        Properties = Type.GetProperties

        If args.Length = 0 Then
            ShowHelp(Type)
            Return 0
        End If

        Dim arguments As New Generic.Queue(Of String)
        For Each arg As String In args
            arguments.Enqueue(arg)
        Next

        Do Until arguments.Count = 0
            Dim arg As String
            arg = arguments.Dequeue

            If arg(0) = "@" Then
                Dim rsp As String
                Dim lines As String()

                rsp = arg.Substring(1)
                lines = IO.File.ReadAllLines(rsp)
                For Each line As String In lines
                    If line.StartsWith("#") = False Then
                        arguments.Enqueue(line)
                    End If
                Next

                Continue Do
            End If

            If arg(0) <> "-" AndAlso arg(0) <> "/" Then
                ShowHelp(Type)
                Return 1
            End If

            Dim name, value As String
            Dim argument As String
            Dim splitter As Integer

            argument = arg.Substring(1)
            splitter = argument.IndexOfAny(New Char() {":"c, "="c})
            If splitter >= 0 Then
                name = argument.Substring(0, splitter)
                value = argument.Substring(splitter + 1)
            Else
                name = argument
                value = String.Empty
            End If

            Select Case name.ToUpperInvariant
                Case "HELP", "H", "?"
                    ShowHelp(Type)
                    Return 0
            End Select

            Dim found As Boolean = False
            For Each prop As PropertyInfo In Properties
                If prop.Name.ToLowerInvariant = name.ToLowerInvariant OrElse Char.ToLowerInvariant(prop.Name(0)) = name Then
                    Dim objValue As Object

                    If prop.PropertyType Is GetType(Boolean) Then
                        Select Case value.ToUpperInvariant
                            Case "Y", "YES", "+", "ON", "TRUE", ""
                                objValue = True
                            Case "N", "NO", "-", "OFF", "FALSE"
                                objValue = False
                            Case Else
                                ShowHelp(Type)
                                Return 1
                        End Select
                    ElseIf prop.PropertyType Is GetType(String) Then
                        objValue = value
                    Else
                        objValue = value
                    End If

                    prop.SetValue(rt, objValue, Nothing, Nothing, Nothing, Nothing)
                    found = True
                    Exit For
                End If
            Next
            If Not found Then
                Console.WriteLine("The option '" & name & "' is unknown.")
            End If
        Loop

        If rt.Run Then
            Return 0
        Else
            Return 1
        End If
    End Function


    Sub ShowHelp(ByVal Type As Type)
        Dim properties As PropertyInfo()

        Console.WriteLine("rt-console")

        properties = Type.GetProperties()
        For Each prop As PropertyInfo In properties
            Dim attrib As ArgumentAttribute
            Dim attribs() As Object
            Dim name As String

            attribs = prop.GetCustomAttributes(GetType(ArgumentAttribute), True)

            If attribs.Length <= 0 Then Continue For

            attrib = TryCast(attribs(0), ArgumentAttribute)
            If attrib Is Nothing Then Continue For

            name = prop.Name.ToLowerInvariant

            Console.Write(vbTab & "-" & name(0) & "[" & name.Substring(1) & "]")
            If attrib.Default IsNot Nothing Then
                Console.Write("[")
            End If
            If prop.PropertyType Is GetType(String) Then
                Console.Write("=<" & attrib.Description & ">)")
            ElseIf prop.PropertyType Is GetType(Boolean) Then
                Console.Write("=on|off (" & attrib.Description & ")")
            Else
                Console.Write("=<" & attrib.Description & ">)")
            End If
            If attrib.Default IsNot Nothing Then
                Console.Write("]")
            End If
            Console.WriteLine("")
        Next

    End Sub
End Module

Class ArgumentAttribute
    Inherits Attribute

    Private m_Description As String
    Private m_Default As Object

    ReadOnly Property Description() As String
        Get
            Return m_Description
        End Get
    End Property

    ReadOnly Property [Default]() As Object
        Get
            Return m_Default
        End Get
    End Property

    Sub New(ByVal Description As String)
        m_Description = Description
    End Sub

    Sub New(ByVal Description As String, ByVal [Default] As Object)
        m_Description = Description
        m_Default = [Default]
    End Sub
End Class

Class rt_console

    Private m_AC As String
    Private m_BasePath As String
    Private m_Compiler As String
    Private m_FailedOutput As Boolean = True
    Private m_GenerateDir As String
    Private m_PEVerify As String
    Private m_Recursive As Boolean = True
    Private m_VBC As String
    Private m_Verbosity As String = "All"
    Private m_PrintStatus As Boolean
    Private m_PrintStatusSkip As String
    Private m_Counters() As Integer
    Private m_Skip As String = "Unfixable"

    <Argument("A colon-delimited list of directories to skip.")> _
    Property Skip() As String
        Get
            Return m_Skip
        End Get
        Set(ByVal value As String)
            m_Skip = value
        End Set
    End Property

    <Argument("A colon-delimited list of states to skip when printing statuses.")> _
    Property PrintStatusSkip() As String
        Get
            Return m_PrintStatusSkip
        End Get
        Set(ByVal value As String)
            m_PrintStatusSkip = value
        End Set
    End Property

    <Argument("Show the current status of all the files.")> _
    Property PrintStatus() As Boolean
        Get
            Return m_PrintStatus
        End Get
        Set(ByVal value As Boolean)
            m_PrintStatus = value
        End Set
    End Property

    <Argument("Show all output from the test if failed.")> _
    Property FailedOutput() As Boolean
        Get
            Return m_FailedOutput
        End Get
        Set(ByVal value As Boolean)
            m_FailedOutput = value
        End Set
    End Property

    <Argument("Path to ac.exe (used to compare any differences with vbc.exe compiled assemblies).")> _
    Property AC() As String
        Get
            Return m_AC
        End Get
        Set(ByVal value As String)
            m_AC = value
        End Set
    End Property

    <Argument("Path to vbc.exe (used to test that the tests are correct)")> _
    Property VBC() As String
        Get
            Return m_VBC
        End Get
        Set(ByVal value As String)
            m_VBC = value
        End Set
    End Property

    <Argument("The amount of output when a test fails: [All|Failed|None], All: all verifications are printed, Failed: only the failed verification, None: guess what!")> _
    Property Verbosity() As String
        Get
            Return m_Verbosity
        End Get
        Set(ByVal value As String)
            m_Verbosity = value
        End Set
    End Property

    <Argument("Path to the compiler to use.")> _
    Property Compiler() As String
        Get
            Return m_Compiler
        End Get
        Set(ByVal value As String)
            m_Compiler = value
        End Set
    End Property

    <Argument("Path to PEVerify.exe")> _
    Property PEVerify() As String
        Get
            Return m_PEVerify
        End Get
        Set(ByVal value As String)
            m_PEVerify = value
        End Set
    End Property

    <Argument("Path to test or test directory to run")> _
    Property BasePath() As String
        Get
            Return m_BasePath
        End Get
        Set(ByVal value As String)
            m_BasePath = value
        End Set
    End Property

    <Argument("Path to directory of test generators")> _
    Property GenerateDir() As String
        Get
            Return m_GenerateDir
        End Get
        Set(ByVal value As String)
            m_GenerateDir = value
        End Set
    End Property

    <Argument("Recurse test directory for tests", True)> _
    Property Recursive() As Boolean
        Get
            Return m_Recursive
        End Get
        Set(ByVal value As Boolean)
            m_Recursive = value
        End Set
    End Property

    Private Sub ValidateArguments()
        If m_Compiler = String.Empty Then
            m_Compiler = "vbnc"
        Else
            m_Compiler = IO.Path.GetFullPath(m_Compiler)
            If IO.File.Exists(m_Compiler) = False Then
                Throw New IO.FileNotFoundException("Compiler '" & m_Compiler & "' does not exist.")
            End If
        End If
    End Sub

    Private Sub ShowSummary(ByVal Duration As TimeSpan)
        Console.WriteLine("Summary:")
        Dim total As Integer
        For i As Integer = 0 To UBound(m_Counters)
            Console.WriteLine("    " & CType(i, Test.Results).ToString() & ": " & m_Counters(i) & " tests.")
            total += m_Counters(i)
        Next
        Console.WriteLine("    Total: " & total.ToString & " tests.")
        Console.WriteLine("    Duration: " & Duration.ToString)
    End Sub

    Function Run() As Boolean
        ValidateArguments()

        If IO.File.Exists(m_BasePath) = False AndAlso IO.Directory.Exists(m_BasePath) = False Then
            Throw New IO.FileNotFoundException("File or directory '" & m_BasePath & "' does not exist (Basepath)")
        End If

        ReDim m_Counters([Enum].GetValues(GetType(Test.Results)).Length - 1)

        m_BasePath = IO.Path.GetFullPath(m_BasePath)
        Test.DirectoriesToSkip = m_Skip.Split(";"c)

        If CBool(IO.File.GetAttributes(m_BasePath) And IO.FileAttributes.Directory) Then
            If IO.Path.GetDirectoryName(m_BasePath) <> Environment.CurrentDirectory Then
                Environment.CurrentDirectory = m_BasePath
            End If
            Return RunDirectory()
        Else
            If IO.Path.GetDirectoryName(m_BasePath) <> Environment.CurrentDirectory Then
                Environment.CurrentDirectory = IO.Path.GetDirectoryName(m_BasePath)
                m_BasePath = IO.Path.GetFileName(m_BasePath)
            End If

            Return RunFile()
        End If
    End Function

    Private Function RunDirectory() As Boolean
        Dim result As Boolean = True
        Dim start As Date = Date.Now

        result = RunDirectory(Nothing, m_BasePath) AndAlso result

        ShowSummary(Date.Now - start)

        Return result
    End Function

    Private Function RunDirectory(ByVal Parent As Tests, ByVal Directory As String) As Boolean
        Dim tests As Tests
        Dim result As Boolean = True

        If m_PrintStatus = False Then
            Console.WriteLine("Loading directory " & Directory & "... ")
        End If

        Try
            If Parent IsNot Nothing Then Exit Try

            Dim parentDir As String = IO.Path.GetDirectoryName(Directory)
            If Directory.EndsWith(IO.Path.DirectorySeparatorChar) Then
                parentDir = IO.Path.GetDirectoryName(parentDir)
            End If
            Dim knownFailures As String = IO.Path.Combine(parentDir, "KnownFailures.txt")
            Console.WriteLine("Checking " & parentDir & " for KnownFailures.txt")
            If IO.File.Exists(knownFailures) Then
                Console.WriteLine(" Found KnownFailures.txt in parent directory (" & parentDir & "), loading that directory too...")
                Parent = New Tests(Nothing, parentDir, m_Compiler, m_VBC, False)
            End If
        Catch ex As Exception
        End Try

        tests = New Tests(Parent, Directory, m_Compiler, m_VBC, False)
        If m_PrintStatus = False Then
            Console.WriteLine(tests.Count & " tests found.")
        End If
        For Each test As Test In tests
            result = RunTest(test) AndAlso result
        Next

        If m_Recursive Then
            For Each dir As String In tests.GetContainedTestDirectories
                result = RunDirectory(tests, dir) AndAlso result
            Next
        End If

        Return result
    End Function


    Private Function RunFile() As Boolean
        Dim t As Test

        t = New Test(m_BasePath, Nothing)

        Return RunTest(t)
    End Function

    Private Function ShowStatus(ByVal t As Test) As Boolean
        Dim status As String

        t.LoadOldResults()

        status = t.OldResult.ToString()

        m_Counters(t.OldResult) += 1

        If m_PrintStatusSkip IsNot Nothing AndAlso m_PrintStatusSkip.Contains(status) Then Return True

        SetColor(t.OldResult)
        Console.Write("{0,-10} ", status & ":")
        Console.ResetColor()
        Console.WriteLine(IO.Path.Combine(IO.Path.GetDirectoryName(t.Files(0).Substring(m_BasePath.Length)), t.Name))

        Return True
    End Function

    Private Sub SetColor(ByVal result As Test.Results)
        Select Case result
            Case Test.Results.Failed
                Console.ForegroundColor = ConsoleColor.Red
            Case Test.Results.Running
                Console.ForegroundColor = ConsoleColor.Blue
            Case Test.Results.Success
                Console.ForegroundColor = ConsoleColor.Green
            Case Test.Results.KnownFailureSucceeded
                Console.ForegroundColor = ConsoleColor.DarkCyan 'GreenYellow
            Case Test.Results.NotRun
                Console.ForegroundColor = ConsoleColor.Yellow
            Case Test.Results.Regressed
                Console.ForegroundColor = ConsoleColor.Magenta 'Indigo
            Case Test.Results.Skipped
                Console.ForegroundColor = ConsoleColor.DarkYellow 'Orange
            Case Test.Results.KnownFailureFailed
                Console.ForegroundColor = ConsoleColor.DarkMagenta 'Purple
            Case Else
                Console.ForegroundColor = ConsoleColor.White
        End Select
    End Sub

    Private Function RunTest(ByVal t As Test) As Boolean
        t.Compiler = Compiler

        If m_PrintStatus Then Return ShowStatus(t)
        Console.Write("Running " & t.Name & "... ")
        t.DoTest()

        SetColor(t.Result)
        Console.WriteLine(t.Result.ToString)
        Console.ResetColor()

        m_Counters(t.Result) += 1

        If t.Result = Test.Results.Success OrElse t.Result = Test.Results.Skipped OrElse t.Result = Test.Results.NotRun Then
            Return True
        ElseIf t.Result = Test.Results.Failed OrElse t.Result = Test.Results.Regressed Then
            If m_FailedOutput Then
                Dim vAll, vFailed As Boolean
                vAll = String.Compare(Verbosity, "All", True) = 0
                vFailed = String.Compare(Verbosity, "Failed", True) = 0
                For Each v As VerificationBase In t.Verifications
                    If vAll OrElse (vFailed AndAlso v.Result = False AndAlso v.Run = True) Then
                        'Console.WriteLine(New String(">"c, Console.BufferWidth))
                        Console.WriteLine(v.DescriptiveMessage)
                        'Console.WriteLine(New String("<"c, Console.BufferWidth))
                    End If
                Next
            End If
            Return False
        Else
            Return True
        End If
    End Function
End Class
