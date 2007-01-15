' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If

''' <summary>
''' The compiler
''' </summary>
''' <remarks></remarks>
Public Class Compiler
    Implements IDisposable

    ''' <summary>
    ''' The filename of the resulting assembly.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_OutFilename As String

    ''' <summary>
    ''' The reporting object
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Report As New Report(Me)

    ''' <summary>
    ''' A helper
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Helper As New Helper(Me)

    ''' <summary>
    ''' The parser
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Parser As Parser

    ''' <summary>
    ''' Represents the commandline passed on to the compiler
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CommandLine As New CommandLine(Me)

    ''' <summary>
    ''' The scanner of the code
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Scanner As New Scanner(Me)

    ''' <summary>
    ''' The token manager.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_tm As tm

    '''' <summary>
    '''' The list that contains all the tokens in the code
    '''' </summary>
    '''' <remarks></remarks>
    'Private m_Tokens As New Tokens

    ''' <summary>
    ''' Contains info about all the types and namespaces available.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeManager As New TypeManager(Me)

    ''' <summary>
    ''' The compiling assembly
    ''' </summary>
    ''' <remarks></remarks>
    Friend theAss As AssemblyDeclaration

    ''' <summary>
    ''' The created assembly
    ''' </summary>
    ''' <remarks></remarks>
    Public AssemblyBuilder As System.Reflection.Emit.AssemblyBuilder

    ''' <summary>
    ''' The one and only module in the assembly
    ''' </summary>
    ''' <remarks></remarks>
    Public ModuleBuilder As System.Reflection.Emit.ModuleBuilder

    ''' <summary>
    ''' Represents the conditinal compiler.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ConditionalCompiler As ConditionalCompiler

    Private m_TypeCache As TypeCache

    'Private SequenceCompleted(CompilerSequence.Finished) As Boolean
    Private SequenceTime(CompilerSequence.End) As DateTime

    Private m_TypeResolver As TypeResolution
    Private m_NameResolver As NameResolution

    Private m_SymbolWriter As System.Diagnostics.SymbolStore.ISymbolWriter

    ReadOnly Property OutFileName() As String
        Get
            Return m_OutFilename
        End Get
    End Property

    ReadOnly Property TypeCache() As TypeCache
        Get
            Return m_TypeCache
        End Get
    End Property

    ReadOnly Property SymbolWriter() As System.Diagnostics.SymbolStore.ISymbolWriter
        Get
            Return m_SymbolWriter
        End Get
    End Property

    ''' <summary>
    ''' Contains info about all the types and namespaces available.
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property TypeManager() As TypeManager
        Get
            Return m_TypeManager
        End Get
    End Property

    ''' <summary>
    ''' The global reporting object
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shadows ReadOnly Property Report() As Report
        Get
            Return m_Report
        End Get
    End Property

    Friend ReadOnly Property Parser() As Parser
        Get
            Return m_Parser
        End Get
    End Property

    ''' <summary>
    ''' Represents the commandline passed on to the compiler
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property CommandLine() As CommandLine
        Get
            Return m_CommandLine
        End Get
        Set(ByVal value As CommandLine)
            m_CommandLine = value
        End Set
    End Property

    ''' <summary>
    ''' The scanner of the code
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property Scanner() As Scanner
        Get
            Return m_Scanner
        End Get
    End Property

    '''' <summary>
    '''' The list that contains all the tokens in the code
    '''' </summary>
    '''' <remarks></remarks>
    'Friend ReadOnly Property Tokens() As Tokens
    '    Get
    '        Return m_Tokens
    '    End Get
    'End Property

    Friend Overrides ReadOnly Property tm() As tm
        Get
            Return m_tm
        End Get
    End Property

    Friend ReadOnly Property ConditionalCompiler() As ConditionalCompiler
        Get
            Return m_ConditionalCompiler
        End Get
    End Property

    Friend ReadOnly Property Helper() As Helper
        Get
            Return m_Helper
        End Get
    End Property

    Friend ReadOnly Property TypeResolver() As TypeResolution
        Get
            Return m_TypeResolver
        End Get
    End Property

    Friend ReadOnly Property TypeResolution() As TypeResolution
        Get
            Return m_TypeResolver
        End Get
    End Property

    Friend ReadOnly Property NameResolver() As NameResolution
        Get
            Return m_NameResolver
        End Get
    End Property

    Function HasPassedSequencePoint(ByVal Point As CompilerSequence) As Boolean
        Return SequenceTime(Point) > #1/1/1900#
    End Function

    Private Function CreateTestOutputFilename(ByVal Filename As String, ByVal TestType As String) As String
        Dim dir As String
        dir = IO.Path.GetDirectoryName(Filename)
        If dir = "" Then dir = Environment.CurrentDirectory
        'dir = IO.Path.Combine(IO.Path.GetDirectoryName(Filename), "test output")

        If IO.Directory.Exists(dir) = False Then IO.Directory.CreateDirectory(dir)
        Return IO.Path.Combine(dir, IO.Path.GetFileName(Filename) & "." & TestType & ".output.xml")
    End Function

    Function Compile(ByVal CommandLine As String()) As Integer
        'Try
        'Show the help if there was an error parsing commandline
        If m_CommandLine.Parse(CommandLine) = False Then
            ShowHelp()
            Return 1
        End If
        Return Compile()
        'Catch ex As Exception
        '    ShowExceptionInfo(ex)
        '    Return -1
        'End Try
    End Function

    Friend Function Compile(ByVal Options As CommandLine) As Boolean
        m_CommandLine = Options
        Return Compile() = 0
    End Function

    '    Private Function Compile_Scan() As Boolean
    '        'Scan and tokenize the source files
    '        Dim ScanStarted As Double = VB.Timer
    '        For Each file As CodeFile In CommandLine.Files
    '            Scanner.ScanFile(file, Tokens)
    '        Next
    '        'Scanner.SetCodeEnd(Tokens)

    '        SequenceTime(CompilerSequence.Scanned) = DateTime.Now

    '#If DEBUG Then
    '        If CommandLine.Files.Count = 1 Then
    '            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "File '" & CommandLine.Files(0).FileName & "' scanned in " & Microsoft.VisualBasic.FormatNumber((Microsoft.VisualBasic.Timer - ScanStarted), 3) & " seconds")
    '        Else
    '            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "All files scanned in " & Microsoft.VisualBasic.FormatNumber((Microsoft.VisualBasic.Timer - ScanStarted), 3) & " seconds")
    '        End If
    '        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("{0} lines and {1} characters in total.", Scanner.TotalLineCount, Scanner.TotalCharCount))
    '#End If

    '#If DEBUG AndAlso False Then
    '        'Dump the tokens
    '        Dim xml As Xml.XmlTextWriter
    '        xml = New Xml.XmlTextWriter(CreateTestOutputFilename(m_OutFilename, "tokens"), Text.Encoding.UTF8)
    '        xml.Formatting = System.Xml.Formatting.Indented
    '        DumperXML.Dump(Tokens, xml)
    '        xml.Close()
    '        xml = Nothing
    '#End If

    '        Return Report.Errors = 0
    '    End Function

    Function Compile_CalculateOutputFilename() As Boolean
        If CommandLine.Out = "" Then
            'Get the first filename
            m_OutFilename = CommandLine.Files(0).FileName
            'Strip the extension
            m_OutFilename = m_OutFilename.Substring(0, m_OutFilename.Length - IO.Path.GetExtension(m_OutFilename).Length)

            If m_OutFilename.EndsWith(".") = False Then m_OutFilename &= "."
            'Put on the correct extension
            If CommandLine.Target = vbnc.CommandLine.Targets.Console OrElse CommandLine.Target = vbnc.CommandLine.Targets.Winexe Then
                m_OutFilename &= "exe"
            ElseIf CommandLine.Target = vbnc.CommandLine.Targets.Library Then
                m_OutFilename &= "dll"
            ElseIf CommandLine.Target = vbnc.CommandLine.Targets.Module Then
                m_OutFilename &= "netmodule"
            Else
                Throw New InternalException(Me)
            End If
        Else
            m_OutFilename = CommandLine.Out
        End If
        m_OutFilename = IO.Path.GetFullPath(m_OutFilename)
        Return True
    End Function

    Private Function Compile_CreateAssemblyAndModuleBuilders() As Boolean
        Dim assemblyName As Reflection.AssemblyName

        'assemblyName = New AssemblyName()
        'assemblyName.Name = IO.Path.GetFileNameWithoutExtension(m_OutFilename)

        assemblyName = Me.Assembly.GetName

        AssemblyBuilder = System.AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, System.Reflection.Emit.AssemblyBuilderAccess.Save, IO.Path.GetDirectoryName(m_OutFilename))
        ModuleBuilder = AssemblyBuilder.DefineDynamicModule(assemblyName.Name, IO.Path.GetFileName(m_OutFilename), True)

        If m_CommandLine.DebugInfo <> vbnc.CommandLine.DebugTypes.None Then
            m_SymbolWriter = ModuleBuilder.GetSymWriter()
        End If

        Return True
    End Function

    Private Function Compile_Parse() As Boolean
        Dim result As Boolean = True
        Dim RootNamespace As String

        If CommandLine.RootNamespace = "" Then
            RootNamespace = "" '(IO.Path.GetFileNameWithoutExtension(m_OutFilename))
        Else
            RootNamespace = (CommandLine.RootNamespace)
        End If

        m_Scanner = New Scanner(Me)
        m_ConditionalCompiler = New ConditionalCompiler(Me, m_Scanner)
        m_tm = New tm(Me, m_ConditionalCompiler)
        m_Parser = New Parser(Me)

        theAss = Parser.Parse(RootNamespace)

        SequenceTime(CompilerSequence.Parsed) = DateTime.Now

        vbnc.Helper.Assert(result = (Report.Errors = 0))

#If DEBUG AndAlso False Then
        Dim xml As Xml.XmlTextWriter
        xml = New Xml.XmlTextWriter(CreateTestOutputFilename(m_OutFilename, "parsedtree"), Text.Encoding.UTF8)
        xml.Formatting = System.Xml.Formatting.Indented
        DumperXML.Dump(theAss, xml)
        xml.Close()
        xml = Nothing
#End If

        Return result
    End Function

    Private Function Compile_Resolve() As Boolean
        Dim result As Boolean = True

#If EXTENDEDDEBUG Then
        Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Starting Resolve")
#End If
        result = CommandLine.Imports.ResolveCode(ResolveInfo.Default(Me)) AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = CommandLine.Files.Resolve(ResolveInfo.Default(Me)) AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.CreateImplicitTypes AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.ResolveTypes AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.ResolveTypeReferences AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.CreateImplicitMembers AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.ResolveMembers AndAlso result
        vbnc.Helper.Assert(result = (Report.Errors = 0))
        result = theAss.ResolveCode(ResolveInfo.Default(Me)) AndAlso result

        If CommandLine.NoVBRuntimeRef Then
            m_TypeCache.Init_vbruntime(True)
        End If

        SequenceTime(CompilerSequence.Resolved) = DateTime.Now

        vbnc.Helper.Assert(result = (Report.Errors = 0))

#If DEBUG Then
        Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Finished Resolve")
#End If

#If DEBUG AndAlso False Then
        Dim xml As Xml.XmlTextWriter
        xml = New Xml.XmlTextWriter(CreateTestOutputFilename(m_OutFilename, "typetree"), Text.Encoding.UTF8)
        xml.Formatting = System.Xml.Formatting.Indented
        DumperXML.Dump(theAss, xml)
        xml.Close()
        xml = Nothing
#End If

        Return result
    End Function
#If DEBUG Then
    Private m_Dumper As Dumper
    ReadOnly Property Dumper() As Dumper
        Get
            Return m_Dumper
        End Get
    End Property
#End If

    ''' <summary>
    ''' Compile with the current options.
    ''' </summary>
    ''' <remarks></remarks>
    Function Compile() As Integer
        Dim result As Boolean = True

        Try
            SequenceTime(CompilerSequence.Start) = DateTime.Now

#If DEBUG Then
            'Dump the commandline
            If CommandLine.Dumping Then
                CommandLine.Dump()
            End If
#End If

            'Show help if asked to
            If CommandLine.Help = True Then
                ShowHelp()
                Return 0
            End If

            'Show logo, unless asked not to
            If CommandLine.NoLogo = False Then ShowLogo()

            'Set the culture to en-us to enable correct parsing of numbers, dates, etc.
            Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-us")

            'm_tm = New tm(Me)

            'Exit if no source files were specified
            If m_CommandLine.Files.Count = 0 Then
                Report.ShowMessage(Messages.VBNC2011)
                Return 1
            End If

            'Set the library of the .net system dir
            m_CommandLine.LibPath.Add(GetSystemDir)

            'Load the referenced assemblies
            If Not CommandLine.References.Contains("mscorlib.dll") Then
                CommandLine.References.Add("mscorlib.dll")
            End If
            If CommandLine.NoVBRuntimeRef = False AndAlso CommandLine.References.Contains("Microsoft.VisualBasic.dll") = False Then
                CommandLine.References.Add("Microsoft.VisualBasic.dll")
            End If

            m_Helper = New Helper(Me)
            m_Helper = New Helper(Me)

            'Calculate the output filename
            result = Compile_CalculateOutputFilename() AndAlso result


#If DEBUG Then
            'Errors before we know the out file name cannot be reported.
            Report.XMLFileName = CreateTestOutputFilename(m_OutFilename, "messages")
#End If


            'Load all the referenced assemblies and load all the types and namespaces into the type manager
            m_TypeCache = New TypeCache(Me)
            result = m_TypeManager.LoadReferenced AndAlso result
            m_NameResolver = New NameResolution(Me) 'Must be created after referenced assemblies have been loaded
            m_TypeResolver = New TypeResolution(Me)

            'Parse the code into the type tree
#If DEBUG Then
            Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Starting Parse")
#End If
            result = Compile_Parse() AndAlso result
            If Report.Errors > 0 Then GoTo ShowErrors

            m_TypeManager.LoadCompiledTypes()

            'Resolve the code
#If DEBUG Then
            Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Starting Resolve")
#End If
            result = Compile_Resolve() AndAlso result
            If Report.Errors > 0 Then GoTo ShowErrors

            'Create the assembly and module builders
            result = Compile_CreateAssemblyAndModuleBuilders() AndAlso result

            result = AddResources() AndAlso result
            If result = False Then GoTo ShowErrors

            AddHandler AppDomain.CurrentDomain.TypeResolve, New ResolveEventHandler(AddressOf Me.TypeResolver.TypeResolver)

            'Passed this step no errors should be found...
#If DEBUG Then
            Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Starting Define")
#End If

            result = theAss.DefineTypes AndAlso result
            vbnc.Helper.Assert(result, "DefineTypes failed somehow!")

            result = theAss.DefineTypeParameters AndAlso result
            vbnc.Helper.Assert(result, "DefineTypeParameters failed somehow!")

            result = theAss.DefineTypeHierarchy AndAlso result
            vbnc.Helper.Assert(result, "DefineTypeHierarcht failed somehow!")

            result = theAss.DefineMembers AndAlso result
            vbnc.Helper.Assert(result, "DefineMembers failed somehow!")

#If DEBUG Then
            Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Starting Emit")
#End If

            result = theAss.Emit AndAlso result
            vbnc.Helper.Assert(result, "Emit failed somehow")

            'Create the assembly types
            result = theAss.CreateTypes AndAlso result
            vbnc.Helper.Assert(result)

            RemoveHandler AppDomain.CurrentDomain.TypeResolve, New ResolveEventHandler(AddressOf Me.TypeResolver.TypeResolver)

            If result = False Then
                Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Error creating the assembly!")
                GoTo ShowErrors
            End If

            'Set the main function / entry point
            result = SetMain() AndAlso result
            If result = False Then GoTo ShowErrors

            'Save the assembly
            AssemblyBuilder.Save(IO.Path.GetFileName(m_OutFilename))
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Assembly '{0}' saved successfully to '{1}'.", AssemblyBuilder.FullName, m_OutFilename))

            SequenceTime(CompilerSequence.End) = DateTime.Now

ShowErrors:
            vbnc.Helper.Assert((Report.Errors = 0 AndAlso result = True) OrElse (Report.Errors > 0 AndAlso result = False))

            If Report.Errors > 0 Or Report.Warnings > 0 Then
                Compiler.Report.WriteLine("There were " & Report.Errors.ToString & " errors and " & Report.Warnings.ToString & " warnings.")
            End If
            If Report.Errors = 0 Then
                Compiler.Report.WriteLine("Compilation successful")
                result = True
            Else
                result = False
            End If

EndOfCompilation:
            result = Report.Errors = 0 AndAlso result

            If result Then
                Return 0
            Else
                Return 1
            End If
        Catch ex As TooManyErrorsException
            Report.ShowMessage(Messages.VBNC30041)
            Return 1
        Catch ex As Exception
            ShowExceptionInfo(ex)
            Return -1
        End Try
        vbnc.Helper.Assert(False, "End of program reached!")
        Return 1
    End Function

    Sub ShowExceptionInfo(ByVal ex As Exception)
#If DEBUG Then
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "-------------------------------------------------------------------------------------------------------------------------")
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Unhandled exception(" & ex.GetType.ToString & "): " & ex.Message)
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, ex.StackTrace)
        If ex.InnerException Is Nothing Then
            Compiler.Report.WriteLine("InnerException: (Nothing)")
        Else
            Compiler.Report.WriteLine("InnerException: " & ex.InnerException.ToString)
        End If
        If ex.GetBaseException Is Nothing Then
            Compiler.Report.WriteLine("GetBaseException: (Nothing)")
        ElseIf ex IsNot ex.GetBaseException Then
            Compiler.Report.WriteLine("GetBaseException: " & ex.GetBaseException.ToString)
        End If
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Data.Count: " & ex.Data.Count)
        'Find the line which caused the exception
        Dim strLines(), strLine As String
        If ex.StackTrace IsNot Nothing Then
            strLines = ex.StackTrace.Split(New Char() {VB.Chr(13), VB.Chr(10)}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To strLines.GetUpperBound(0)
                strLine = strLines(i)
                'Remove -at-
                strLine = strLine.Substring(strLine.IndexOf("at ") + 3)
                If strLine.StartsWith("vbnc.", True, Nothing) Then
                    strLine = strLine.Substring(strLine.IndexOf(" in ") + 4)
                    strLine = strLine.Replace(":line ", "(")
                    strLine &= "): " & ex.Message
                    Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, strLine)
                End If
            Next
        End If

        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Commandline arguments:")
        Compiler.Report.Indent()
        For Each arg As String In CommandLine.AllArguments
            If arg.EndsWith(".vb") = False Then
                Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, arg)
            End If
        Next
        Compiler.Report.Unindent()
        Compiler.Report.WriteLine(ex.Message)

        Dim xml As Xml.XmlTextWriter
        Try
            xml = New Xml.XmlTextWriter(CreateTestOutputFilename(m_OutFilename, "exceptions"), System.Text.Encoding.UTF8)
            xml.Formatting = System.Xml.Formatting.Indented
            xml.WriteStartElement("Sequence")

            For i As Integer = CompilerSequence.Start To CompilerSequence.End
                xml.WriteStartElement(CType(i, CompilerSequence).ToString)
                xml.WriteString(SequenceTime(i).ToLongTimeString())
                xml.WriteEndElement()
            Next

            xml.WriteEndElement()

            InternalException.Dump(xml, ex, False)
            xml.Close()
            xml = Nothing
        Catch
            'Just do nothing.
        End Try
#Else
            Compiler.Report.WriteLine("Unexpected error: " & ex.Message)
#End If
    End Sub

    ''' <summary>
    ''' Returns true if the specified MethodInfo is a valid candidate to a Main function.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsMainMethod(ByVal method As Reflection.MethodInfo) As Boolean
        'Only static methods
        If method.IsStatic = False Then Return False
        'Only methods called 'Main'
        If vbnc.Helper.CompareName(method.Name, "Main") = False Then Return False
        'Only methods with no return type or Integer return type
        If method.ReturnType IsNot Nothing AndAlso method.ReturnType IsNot Compiler.TypeCache.Void AndAlso method.ReturnType IsNot Compiler.TypeCache.Integer Then Return False

        'Only methods with no parameters or methods with one String() parameter
        Dim params() As ParameterInfo
        params = method.GetParameters
        If params.Length = 0 Then Return True
        If params.Length > 1 Then Return False
        If params(0).ParameterType Is Compiler.TypeCache.String_Array AndAlso params(0).IsOptional = False AndAlso params(0).IsOut = False Then Return True

        Return False
    End Function

    ReadOnly Property Logo() As String
        Get
            Dim result As New System.Text.StringBuilder
            Dim FileVersion As Diagnostics.FileVersionInfo = Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
#If DEBUG Then
            result.AppendLine(FileVersion.ProductName & " version " & FileVersion.FileVersion & " (last write time: " & IO.File.GetLastWriteTime(FileVersion.FileName).ToString("dd/MM/yyyy HH:mm:ss") & ")")
#Else
            result.AppendLine(FileVersion.ProductName & " version " & FileVersion.FileVersion)
#End If
            result.AppendLine(FileVersion.LegalCopyright)
            result.AppendLine()

            Return result.ToString
        End Get
    End Property

    ReadOnly Property Help() As String
        Get
            Dim result As New System.Text.StringBuilder

            result.AppendLine("")
            result.AppendLine("            Visual Basic .NET Compiler Options          ")
            result.AppendLine("")
            result.AppendLine("                       >>> Output file options >>>")
            result.AppendLine("/out:<filename>        Sets the file name of the output executable or library.")
            result.AppendLine("/target:exe            Create a console application (this is the default). (Short form: /t)")
            result.AppendLine("/target:winexe         Create a Windows application.")
            result.AppendLine("/target:library        Create a library assembly.")
            result.AppendLine("/target:module         Create a module that can be added to an assembly.")
            result.AppendLine("")
            result.AppendLine("                       >>> Input files options >>>")
            result.AppendLine("/addmodule:<filename>  Reference metadata from the specified module.")
            result.AppendLine("/recurse:<wildcard>    Include all files in the current directory and subdirectories according to the wildcard specifications.")
            result.AppendLine("/reference:<file_list> Reference metadata from the specified assembly. (Short form: /r)")
            result.AppendLine("")
            result.AppendLine("                       >>> Resources options >>>")
            result.AppendLine("/linkresource:<resinfo>Links the specified file as an external assembly resource. resinfo:<file>[,<name>[,public|private]] (Short form: /linkres)")
            result.AppendLine("/resource:<resinfo>    Adds the specified file as an embedded assembly resource. resinfo:<file>[,<name>[,public|private]] (Short form: /res)")
            result.AppendLine("/win32icon:<file>      Specifies a Win32 icon file (.ico) for the default Win32 resources.")
            result.AppendLine("/win32resource:<file>  Specifies a Win32 resource file (.res).")
            result.AppendLine("")
            result.AppendLine("                       >>> Debug and code generation options >>>")
            result.AppendLine("/optimize[+|-]         Enable optimizations.")
            result.AppendLine("/removeintchecks[+|-]  Remove integer checks. Default off.")
            result.AppendLine("/debug[+|-]            Emit debugging information.")
            result.AppendLine("/debug:full            Emit full debugging information (default).")
            result.AppendLine("/debug:pdbonly         Emit PDB file only.")
            result.AppendLine("")
            result.AppendLine("                       >>> Errors and warnings options >>>")
            result.AppendLine("/nowarn                Disable warnings.")
            result.AppendLine("/warnaserror[+|-]      Treat warnings as errors.")
            result.AppendLine("")
            result.AppendLine("                       >>> Language options >>>")
            result.AppendLine("/define:<list>         Declare global conditional compilation symbol(s).  list:name=value,... (Also: /d)")
            result.AppendLine("/imports:<list>        Declare global Imports for namespaces in referenced metadata files. list:namespace,...")
            result.AppendLine("/optionexplicit[+|-]   Require explicit declaration of variables.")
            result.AppendLine("/optionstrict[+|-]     Enforce strict language semantics.")
            result.AppendLine("/rootnamespace:<string>sets the root Namespace for all type declarations.")
            result.AppendLine("/optioncompare:binary  Do binary string comparisons. (Default)")
            result.AppendLine("/optioncompare:text    Do text string comparisons.")
            result.AppendLine("")
            result.AppendLine("                       >>> Various options >>> ")
            result.AppendLine("/help                  Show this help message. (Also: /?)")
            result.AppendLine("/nologo                Do not show the compiler copyright banner.")
            result.AppendLine("/quiet                 Specifies a quiet mode - only errors will be shown.")
            result.AppendLine("/verbose               Show verbose messages.")
            result.AppendLine("/noconfig              Disable the automatic inclusion of the vbnc.rsp response file.")
            result.AppendLine("")
            result.AppendLine("                       >>> Advanced options >>>")
            result.AppendLine("/baseaddress:<number>  Specifies the base address of the library or module (in hex).")
            result.AppendLine("/bugreport:<file>      Create bug report file.")
            result.AppendLine("/codepage:<number>     Specifies the codepage to use when opening source files.")
            result.AppendLine("/delaysign[+|-]        Specifies whether to delay-sign the assembly using only the public portion of the strong name key.")
            result.AppendLine("/keycontainer:<string> Specifies a strong name key container. *Not supported yet.*")
            result.AppendLine("/keyfile:<file>        Specifies a strong name key file. *Not supported yet.*")
            result.AppendLine("/libpath:<path_list>   Lists the directories to search for metadata references. (Delimited by semi-colons.")
            result.AppendLine("/main:<class>          Specifies the entry method of the assembly. Can be a Main sub or function, or a class that inherits from System.Windows.Forms.Form. (Also: /m)")
            result.AppendLine("/netcf                 Specifies the .NET Compact Framework as the target. *Not supported*.")
            result.AppendLine("/sdkpath:<path>        where the .Net Framework (mscorlib.dll) is located.")
            result.AppendLine("/utf8output[+|-]       Emit the output from the compiler in UTF8 encoding. *Not supported yet*")
#If DEBUG Then
            result.AppendLine("/dump                  Dump the various outputs. Only avaliable in debug builds.")
#End If

            Return result.ToString
        End Get
    End Property

    ''' <summary>
    ''' Writes the logo of the compiler to the console.
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowLogo()
        Compiler.Report.WriteLine(Logo)
    End Sub

    ''' <summary>
    ''' Writes the help of the compier to the console.
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowHelp()
        ShowLogo()
        Compiler.Report.WriteLine(Help)
    End Sub

    Private Function AddResources() As Boolean
        Dim result As Boolean = True

        For Each r As Resource In CommandLine.Resources
            Dim resourceDescription As String = ""
            Dim resourceFile As String = IO.Path.GetFileName(r.Filename)
            Dim resourceName As String = IO.Path.GetFileName(r.Filename)
            Dim attrib As System.Reflection.ResourceAttributes
            If r.Public Then
                attrib = ResourceAttributes.Public
            Else
                attrib = ResourceAttributes.Private
            End If

            Dim reader As System.Resources.IResourceReader
            Select Case IO.Path.GetExtension(r.Filename).ToLowerInvariant
                Case ".resx"
                    reader = Nothing 'New System.Resources.ResXResourceReader(r.Filename)
                Case ".resources"
                    reader = New System.Resources.ResourceReader(r.Filename)
                Case Else
                    reader = Nothing
            End Select

            'Report.WriteLine("Defining resource, FileName=" & r.Filename & ", Identifier=" & r.Identifier & ", reader is nothing=" & (reader Is Nothing).ToString())
            If reader IsNot Nothing Then
                Dim writer As System.Resources.IResourceWriter = ModuleBuilder.DefineResource(resourceName, resourceDescription, attrib)
                For Each resource As System.Collections.DictionaryEntry In reader
                    'Report.WriteLine(">" & resource.Key.ToString & "=" & resource.Value.ToString)
                    writer.AddResource(resource.Key.ToString, resource.Value)
                Next
                reader.Dispose()
            Else
                'Report.WriteLine(">Writing ManifestResource")
                ModuleBuilder.DefineManifestResource(resourceName, New IO.FileStream(r.Filename, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read), attrib)
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Sets the entry point / Main function of the assembly
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMain() As Boolean
        Dim result As Boolean = True

        If False AndAlso vbnc.Helper.IsOnMono AndAlso CommandLine.Files.Count > 100 Then
            Report.WriteLine("Skipped setting Main method")
            Return True
        End If

        Try
            'Find the main function
            Dim lstMethods As New ArrayList
            Dim methods As Reflection.MethodInfo()
            If CommandLine.Target = vbnc.CommandLine.Targets.Console Then
                If CommandLine.Main <> "" Then
                    Dim mainClasses As ArrayList
                    mainClasses = TypeResolver.LookupType(CommandLine.Main)
                    If mainClasses.Count <> 1 Then
                        Report.ShowMessage(Messages.VBNC90013, CommandLine.Main, mainClasses.Count.ToString)
                        Return False
                    End If

                    Dim mainClass As Type = CType(mainClasses(0), Type)
                    methods = mainClass.GetMethods

                    For Each m As Reflection.MethodInfo In methods
                        If IsMainMethod(m) Then lstMethods.Add(m)
                    Next
                Else
                    Dim tps() As Type = AssemblyBuilder.GetTypes
                    For Each t As Type In tps
                        methods = t.GetMethods
                        For Each m As Reflection.MethodInfo In methods
                            If IsMainMethod(m) Then lstMethods.Add(m)
                        Next
                    Next
                End If
                'Set the entry point of the assembly
                Dim entryMethod As Reflection.MethodInfo
                If lstMethods.Count > 1 Then
                    Report.ShowMessage(Messages.VBNC30738, theAss.Name)

                    If (CommandLine.Target = vbnc.CommandLine.Targets.Console OrElse CommandLine.Target = vbnc.CommandLine.Targets.Winexe) Then
                        Report.ShowMessage(Messages.VBNC30420, theAss.Name)
                        Return False
                    Else
                        'CHECK: is this something to report? Report.Warning("No shared 'Main' method was found!")
                    End If
                ElseIf lstMethods.Count = 0 Then
                    If (CommandLine.Target = vbnc.CommandLine.Targets.Console OrElse CommandLine.Target = vbnc.CommandLine.Targets.Winexe) Then
                        Report.ShowMessage(Messages.VBNC30420, theAss.Name)
                        Return False
                    Else
                        ' Report.Warning("No shared 'Main' method was found!")
                    End If
                Else
                    entryMethod = CType(lstMethods(0), Reflection.MethodInfo)
                    AssemblyBuilder.SetEntryPoint(entryMethod)
                End If
            ElseIf CommandLine.Target = vbnc.CommandLine.Targets.Winexe Then
                'Find a class inheriting from System.Forms.Form.
                Throw New NotImplementedException
            End If

        Catch ex As Exception
            vbnc.Helper.NotImplementedYet(ex.Message)
            Throw
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Returns the directory where the system assemblies are installed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSystemDir() As String
        Dim assemblies() As Reflection.Assembly = AppDomain.CurrentDomain.GetAssemblies

        For Each a As Reflection.Assembly In assemblies
            Dim codebase As String = a.Location
            If codebase.EndsWith("corlib.dll") Then
                Return codebase.Substring(0, codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
            End If
        Next
        Throw New InternalException("Cannot compute the system directory.")
        Return ""
    End Function

#Region " IDisposable Support "
    Private disposed As Boolean

    ' IDisposable
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
#If DEBUG Then
                Report.Flush()
#End If
            End If
        End If
        Me.disposed = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

End Class
