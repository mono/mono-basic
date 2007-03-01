Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Diagnostics

Module win_make

    Public ValidTargets As String() = New String() {"all", "test", "run-test"}
    Public ValidProfiles As String() = New String() {"net_1_0", "net_1_1", "net_2_0"}

    Function Main(ByVal Args() As String) As Integer
        Try
            Dim Builder As New Builder
            Console.WriteLine("win-make")

            For Each str As String In Args
                Console.WriteLine("Parsing: " & str)

                Dim split As Integer = str.IndexOfAny(New Char() {":"c, "="c})
                Dim name, value As String

                If split <= 0 Then
                    Builder.Targets.Add(str)
                Else
                    name = str.Substring(0, split)
                    value = str.Substring(split + 1)

                    Select Case name.ToLower
                        Case "profile"
                            Builder.Profile = value
                        Case "exclude"
                            Builder.Excludes.Add(value)
                        Case "include"
                            Builder.Includes.Add(value)
                        Case "define"
                            Builder.Defines.Add(value)
                        Case "runtime"
                            Builder.Runtime = value
                        Case "compiler"
                            Builder.Compiler = value
                        Case Else
                            Console.WriteLine("Unknown option: " & name)
                            Return 1
                    End Select
                End If

            Next

            Builder.Build()

#If NET_VER >= 2.0 Then
            Console.ForegroundColor = ConsoleColor.Green
#End If
            Console.WriteLine("Build succeeded")
#If NET_VER >= 2.0 Then
            Console.ResetColor
#End If
        Catch ex As Exception
            Console.WriteLine(ex.Message & vbNewLine & ex.StackTrace)
            Return 1
        End Try
    End Function

End Module


Class Builder
    Public BaseDirectory As String
    Public Profile As String
    Public Targets As New ArrayList
    Public Echo As Boolean
    Public Excludes As New ArrayList
    Public Includes As New ArrayList
    Public ResponseFile As String = "@vbruntime.rsp"
    Public NUnitPath As String = "nunit-console.exe"
    Public Sources As String = "@Microsoft.VisualBasic.dll.sources.win"
    Public Defines As New ArrayList
    Public CSDefines As New ArrayList
    Public Runtime As String
    Public Compiler As String
    Public MSRuntimePath As String
    Public MSCompiler As String
    Public CSCompiler As String
    Public MonoCompiler As String
    Public CompilerPath As String

    Sub Build()
        Console.WriteLine("Building...")
        ' Find the base directory
        For Each line As String In New IO.StreamReader("Makefile").ReadToEnd.Split(Chr(10), Chr(13))
            If line.Trim.StartsWith("thisdir") Then
                line = line.Substring(9).TrimStart("="c, " "c).Trim
                BaseDirectory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - line.Length)
                Exit For
            End If
        Next
        Console.WriteLine("BaseDirectory=" & BaseDirectory)
        Console.WriteLine("CurrentDirectory=" & Environment.CurrentDirectory)

        If Profile = "" Then Profile = "net_2_0"

        Select Case Profile
            Case "net_1_0"
                Defines.Add("NET_1_0")
                CSDefines.AddRange(Defines)
                Defines.Add("NET_VER=1.0")
                MSRuntimePath = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%WINDIR%"), "Microsoft.Net\Framework\v1.0.3705\")
                If Compiler = "" Then Compiler = "vbc"
            Case "net_1_1"
                Defines.Add("NET_1_0")
                Defines.Add("NET_1_1")
                CSDefines.AddRange(Defines)
                Defines.Add("NET_VER=1.1")
                MSRuntimePath = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%WINDIR%"), "Microsoft.Net\Framework\v1.1.4322\")
                If Compiler = "" Then Compiler = "vbc"
            Case "net_2_0"
                Defines.Add("NET_1_0")
                Defines.Add("NET_1_1")
                Defines.Add("NET_2_0")
                CSDefines.AddRange(Defines)
                Defines.Add("NET_VER=2.0")
                MSRuntimePath = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%WINDIR%"), "Microsoft.Net\Framework\v2.0.50727\")
                If Compiler = "" Then Compiler = "vbnc"
        End Select
        Console.WriteLine("Profile=" & Profile)

        MSCompiler = IO.Path.Combine(MSRuntimePath, "vbc.exe")
        CSCompiler = IO.Path.Combine(MSRuntimePath, "csc.exe")
        Console.WriteLine("MSCompiler=" & MSCompiler)
        Console.WriteLine("CSCompiler=" & CSCompiler)


        MonoCompiler = IO.Path.Combine(BaseDirectory, "vbnc\vbnc\bin\vbnc.exe")
        If Not IO.File.Exists(MonoCompiler) Then
            MonoCompiler = IO.Path.Combine(BaseDirectory, "class\lib\vbnc\vbnc.exe")
            If Not IO.File.Exists(MonoCompiler) Then
                MonoCompiler = IO.Path.Combine(BaseDirectory, "class\lib\bootstrap\vbnc.exe")
            End If
        End If
        Console.WriteLine("MonoCompiler=" & MonoCompiler)

        Select Case Compiler
            Case "vbc"
                CompilerPath = MSCompiler
            Case "vbnc"
                CompilerPath = MonoCompiler
            Case Else
                Throw New ArgumentException("Compiler can be either 'vbc' or 'vbnc'")
        End Select
        Console.WriteLine("Compiler=" & Compiler)
        Console.WriteLine("CompilerPath=" & CompilerPath)

        If Targets.Count = 0 Then
            Targets.Add("all")
        End If

        For Each target As String In Targets
            Select Case target
                Case "all"
                    BuildAll()
                Case "test"
                    BuildTest()
                Case "run-test"
                    BuildRunTest()
            End Select
        Next
    End Sub

    Sub BuildAll()
        Console.WriteLine("BuildAll")
        Environment.CurrentDirectory = IO.Path.Combine(BaseDirectory, "vbruntime\Microsoft.VisualBasic")
        Execute(CompilerPath, Sources, ResponseFile, "-define:mono_not_yet," & Join(Defines.ToArray(), ","))
    End Sub

    Sub BuildTest()
        Console.WriteLine("BuildTest")
        BuildAll()

        Dim NUnitLibDir As String = IO.Path.Combine(BaseDirectory, "vbruntime\Test\bin")
        Environment.CurrentDirectory = IO.Path.Combine(BaseDirectory, "vbruntime\Test")


        If Runtime <> "ms" Then
            IO.File.Copy(IO.Path.Combine(BaseDirectory, "vbruntime\Microsoft.VisualBasic\Microsoft.VisualBasic.dll"), IO.Path.Combine(Environment.CurrentDirectory, "Microsoft.VisualBasic.dll"), True)
        End If

        Execute(IO.Path.Combine(BaseDirectory, "tools\extract-source\extract-source.exe"), "-source:2005VB_test_VB.vbproj", "-destination:2005VB_test_VB.dll.sources.win", "-mode:w")
        Execute(IO.Path.Combine(BaseDirectory, "tools\extract-source\extract-source.exe"), "-source:2005VB_test_CS.csproj", "-destination:2005VB_test_CS.dll.sources.win", "-mode:w")
        Execute(CSCompiler, "-out:bin\2005VB_test_CS.dll", "@2005VB_test_CS.dll.rsp", "@2005VB_test_CS.dll.sources.win", "/lib:" & NUnitLibDir, "-define:" & Join(CSDefines.ToArray, ","))
        Execute(CompilerPath, "-out:bin\2005VB_test_VB.dll", "@2005VB_test_VB.dll.rsp", "@2005VB_test_VB.dll.sources.win", "/libpath:" & NUnitLibDir, "-define:" & Join(Defines.ToArray, ","))
    End Sub

    Sub BuildRunTest()
        Console.WriteLine("BuildRunTest")
        BuildTest()

        Environment.CurrentDirectory = IO.Path.Combine(BaseDirectory, "vbruntime\Test\bin")

        If Runtime = "ms" Then
            IO.File.Delete(IO.Path.Combine(Environment.CurrentDirectory, "Microsoft.VisualBasic.dll"))
            Excludes.Add("NotDotNet")
        Else
            IO.File.Copy(IO.Path.Combine(BaseDirectory, "vbruntime\Microsoft.VisualBasic\Microsoft.VisualBasic.dll"), IO.Path.Combine(Environment.CurrentDirectory, "Microsoft.VisualBasic.dll"), True)
        End If

        Dim TestDirectory As String = IO.Path.Combine(IO.Path.GetTempPath, "vbruntime-testdir")
        If IO.Directory.Exists(TestDirectory) Then
            IO.Directory.Delete(TestDirectory, True)
        End If
        IO.Directory.CreateDirectory(TestDirectory)

        Dim patterns As New ArrayList(New String() {"*.dll", "*.exe", "*.pdb"})
        If Profile = "net_2_0" Then patterns.Add("*.exe.config")
        For Each pattern As String In patterns
            For Each file As String In IO.Directory.GetFiles(Environment.CurrentDirectory, pattern)
                IO.File.Copy(file, IO.Path.Combine(TestDirectory, IO.Path.GetFileName(file)), True)
            Next
        Next

        Environment.CurrentDirectory = TestDirectory

        Dim excl As String = "", incl As String = ""
        If Includes.Contains("UI") = False Then Excludes.Add("UI")
        If Includes.Contains("InternetRequired") = False Then Excludes.Add("InternetRequired")
        If Excludes.Count > 0 Then
            excl = "-exclude=" & Join(Excludes.ToArray, ",")
        End If
        If Includes.Count > 0 Then
            incl = "-include=" & Join(Includes.ToArray, ",")
        End If

        Execute(NUnitPath, False, "2005VB_test_CS.dll", excl, incl)
        Execute(NUnitPath, False, "2005VB_test_VB.dll", excl, incl)

    End Sub

    Sub Execute(ByVal Program As String, ByVal ParamArray Args As String())
        Execute(Program, True, Args)
    End Sub

    Sub Execute(ByVal Program As String, ByVal TryAssemblyLoad As Boolean, ByVal ParamArray Args As String())
        Dim Arguments As String = Join(Args, " ")
        Console.WriteLine("Executing: " & Program & " " & Arguments)

        'Try to load the executable as an assembly
        If TryAssemblyLoad Then
            Try
                Dim exe As Reflection.Assembly
                Dim result As Object
                exe = Reflection.Assembly.LoadFile(Program)
                result = exe.EntryPoint.Invoke(Nothing, New Object() {Args})
                If CInt(result) <> 0 Then
                    Console.WriteLine("Last command failed with exit code: " & CInt(result).ToString)
                    Environment.Exit(CInt(result))
                End If
            Catch ex As Exception
                'Console.WriteLine("Could not load the program as an .NET assemly: " & ex.Message)
            End Try
        End If

        Dim p As System.Diagnostics.Process = New Process()
        Try
            p.StartInfo.Arguments = Arguments
            p.StartInfo.FileName = Program
            p.StartInfo.UseShellExecute = False
            p.Start()
            p.WaitForExit()
            If p.ExitCode <> 0 Then
#If NET_VER >= 2.0 Then
                Console.ForegroundColor = ConsoleColor.Red
#End If
                Console.WriteLine("Last command failed with exit code: " & p.ExitCode.ToString)
#If NET_VER >= 2.0 Then
                Console.ResetColor ()
#End If
                Environment.Exit(p.ExitCode)
            End If
        Finally
            If Not p Is Nothing Then p.Dispose()
        End Try
    End Sub
End Class
