Imports System.Reflection

Module MainModule

    Function Main(ByVal args() As String) As Integer
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
                value = Nothing
            End If

            Select Case name.ToUpperInvariant
                Case "HELP", "H", "?"
                    ShowHelp(Type)
                    Return 0
            End Select

            For Each prop As PropertyInfo In Properties
                If prop.Name.ToLowerInvariant = name.ToLowerInvariant Then
                    Dim objValue As Object

                    If prop.PropertyType Is GetType(Boolean) Then
                        Select Case value.ToUpperInvariant
                            Case "Y", "YES", "+", "ON", "TRUE"
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

                    prop.SetValue(rt, value, Nothing, Nothing, Nothing, Nothing)

                    Exit For
                End If
            Next
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

    Private m_Counters() As Integer

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

    Sub ValidateArguments()
        If m_Compiler = String.Empty Then
            m_Compiler = "vbnc"
        End If
    End Sub

    Sub ShowSummary(ByVal Duration As TimeSpan)
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
            Throw New IO.FileNotFoundException("File or directory does not exist.", m_BasePath)
        End If

        ReDim m_Counters([Enum].GetValues(GetType(Test.Results)).Length - 1)

        If CBool(IO.File.GetAttributes(m_BasePath) And IO.FileAttributes.Directory) Then
            Return RunDirectory()
        Else
            Return RunFile()
        End If
    End Function

    Function RunDirectory() As Boolean
        Dim result As Boolean = True
        Dim start As Date = Date.Now

        result = RunDirectory(m_BasePath) AndAlso result

        ShowSummary(Date.Now - start)

        Return result
    End Function

    Function RunDirectory(ByVal Directory As String) As Boolean
        Dim tests As Tests
        Dim result As Boolean = True

        Console.Write("Loading directory " & Directory & "... ")
        tests = New Tests(Directory, m_Compiler, m_VBC, False)
        Console.WriteLine(tests.Count & " tests found.")
        For Each test As Test In tests
            result = RunTest(test) AndAlso result
        Next

        If m_Recursive Then
            For Each dir As String In tests.GetContainedTestDirectories
                result = RunDirectory(dir) AndAlso result
            Next
        End If

        Return result
    End Function


    Function RunFile() As Boolean
        Dim t As Test

        t = New Test(m_BasePath, Nothing)

        Return RunTest(t)
    End Function

    Function RunTest(ByVal t As Test) As Boolean
        t.Compiler = Compiler
        Console.Write("Running " & t.Name & "... ")
        t.DoTest()

        Console.WriteLine(t.Result.ToString)

        m_Counters(t.Result) += 1

        If t.Result = Test.Results.Success Then
            Return True
        Else
            If m_FailedOutput Then
                For Each v As VerificationBase In t.Verifications
                    Console.WriteLine("Verification " & v.Name & " " & IIf(v.Result, "Success", "Failed").ToString())
                    Console.WriteLine(v.DescriptiveMessage)
                Next
            End If
            Return False
        End If
    End Function
End Class
