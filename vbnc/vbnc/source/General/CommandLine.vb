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

Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

'        Visual Basic .NET Compiler Options

'                        - OUTPUT FILE -
'/out:<file>             Specifies the output file name.
'/target:exe             Create a console application (default). (Short form: /t)
'/target:winexe          Create a Windows application.
'/target:library         Create a library assembly.
'/target:module          Create a module that can be added to an assembly.
'/doc[+|-]               Generates XML documentation file.
'/doc:<file>             Generates XML documentation file to <file>.

'                        - INPUT FILES -
'/addmodule:<file>       Reference metadata from the specified module.
'/recurse:<wildcard>     Include all files in the current directory and subdirectories according to the wildcard specifications.
'/reference:<file_list>  Reference metadata from the specified assembly. (Short form: /r)

'                        - RESOURCES -
'/linkresource:<resinfo> Links the specified file as an external assembly resource. resinfo:<file>[,<name>[,public|private]] (Short form: /linkres)
'/nowin32manifest        The default manifest should not be embedded in the manifest section of the output PE.
'/resource:<resinfo>     Adds the specified file as an embedded assembly resource. resinfo:<file>[,<name>[,public|private]] (Short form: /res)
'/win32icon:<file>       Specifies a Win32 icon file (.ico) for the default Win32 resources.
'/win32manifest:<file>   The provided file is embedded in the manifest section of the output PE.
'/win32resource:<file>   Specifies a Win32 resource file (.res).

'                        - CODE GENERATION -
'/optimize[+|-]          Enable optimizations.
'/removeintchecks[+|-]   Remove integer checks. Default off.
'/debug[+|-]             Emit debugging information.
'/debug:full             Emit full debugging information (default).
'/debug:pdbonly          Emit PDB file only.

'                        - ERRORS AND WARNINGS -
'/nowarn                 Disable warnings.
'/nowarn:<number_list>   Disable a list of individual warnings.
'/warnaserror[+|-]       Treat warnings as errors.
'/warnaserror[+|-]:<number_list> Treat a list of warnings as errors.

'                        - LANGUAGE -
'/define:<symbol_list>   Declare global conditional compilation symbol(s). symbol_list:name=value,... (Short form: /d)
'/imports:<import_list>  Declare global Imports for namespaces in referenced metadata files. import_list:namespace,...
'/optionexplicit[+|-]    Require explicit declaration of variables.
'/optioninfer[+|-]       Allow type inference of variables.
'/optionstrict[+|-]      Enforce strict language semantics.
'/optionstrict:custom    Warn when strict language semantics are not respected.
'/rootnamespace:<string> Specifies the root Namespace for all type declarations.
'/optioncompare:binary   Specifies binary-style string comparisons. This is the default.
'/optioncompare:text     Specifies text-style string comparisons.

'                        - MISCELLANEOUS -
'/help                   Display this usage message. (Short form: /?)
'/noconfig               Do not auto-include VBC.RSP file.
'/nologo                 Do not display compiler copyright banner.
'/quiet                  Quiet output mode.
'/verbose                Display verbose messages.
'/trace                  Output trace messages (vbnc extension)

'                        - ADVANCED -
'/baseaddress:<number>   The base address for a library or module (hex).
'/bugreport:<file>       Create bug report file.
'/codepage:<number>      Specifies the codepage to use when opening source files.
'/delaysign[+|-]         Delay-sign the assembly using only the public portion of the strong name key.
'/errorreport:<string>   Specifies how to handle internal compiler errors; must be prompt, send, none or queue (default).
'/keycontainer:<string>  Specifies a strong name key container.
'/keyfile:<file>         Specifies a strong name key file.
'/libpath:<path_list>    List of directories to search for metadata references. (Semi-colon delimited.)
'/main:<class>           Specifies the Public Class or Module that contains Sub Main. It can also be a Public Class that inherits from System.Windows.Forms.Form. (Short form: /m)
'/netcf                  Target the .NET Compact Framework.
'/nostdlib               Do not reference standard libraries (system.dll and VBC.RSP file).
'/platform:<string>      Limit which platforms this code can run on; must be x86, x64, Itanium or anycpu (default).
'/sdkpath:<path>         Location of the .NET Framework SDK directory (mscorlib.dll).
'/utf8output[+|-]        Emit compiler output in UTF8 character encoding.
'@<file>                 Insert command-line settings from a text file.
'/vbruntime[+|-]         Compile with/without the default Visual Basic runtime.
'/vbruntime:<file>       Compile with the alternate Visual Basic runtime in <file>.g:



''' <summary>
''' 
''' </summary>
Public Class CommandLine

    Private Shared PATTERNCHARS As Char() = New Char() {"*"c, "?"c}

    ''' <summary>
    ''' There can be many response files, including response files called
    ''' from a response file. Keep a list of them all to know when a
    ''' response file already has been parsed, to avoid stack problems 
    ''' (and a response file shouldn't be included twice anyway)
    ''' </summary>
    Private m_lstResponseFiles As New Specialized.StringCollection

    ''' <summary>
    ''' All files specified
    ''' </summary>s
    ''' <remarks></remarks>
    Private m_lstFileNames As CodeFiles

    ''' <summary>
    ''' A list of all the arguments parsed.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_lstAllArgs As New Specialized.StringCollection

    ''' <summary>
    ''' The compiler used.
    ''' </summary>
    Private m_Compiler As Compiler

    ''' <summary>
    ''' The compiler used.
    ''' </summary>
    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

#Region "Properties"

    ' - OUTPUT FILE -

    ''' <summary>
    ''' /out:&lt;file&gt;             Specifies the output file name.
    ''' </summary>
    Private m_strOut As String

    ''' <summary>
    ''' /target:exe             Create a console application (default). (Short form: /t)
    ''' /target:winexe          Create a Windows application.
    ''' /target:library         Create a library assembly.
    ''' /target:module          Create a module that can be added to an assembly.
    ''' </summary>
    Private m_strTarget As Targets

    ''' <summary>
    ''' /doc[+|-]               Generates XML documentation file.
    ''' /doc:(file)             Generates XML documentation file to (file).
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDoc As String

    ' - INPUT FILES -

    ''' <summary>
    ''' /addmodule:&lt;file&gt;       Reference metadata from the specified module.
    ''' </summary>
    Private m_lstModules As New Specialized.StringCollection

    ''' <summary>
    ''' /recurse:&lt;wildcard&gt;     Include all files in the current directory and subdirectories according to the wildcard specifications.
    ''' </summary>
    Private m_lstRecurse As New Specialized.StringCollection

    ''' <summary>
    ''' /reference:&lt;file_list&gt;  Reference metadata from the specified assembly. (Short form: /r)
    ''' </summary>
    Private m_lstReferences As New Specialized.StringCollection

    ' - RESOURCES -

    ''' <summary>
    ''' /linkresource:&lt;resinfo&gt; Links the specified file as an external assembly resource. resinfo:&lt;file&gt;[,&lt;name&gt;[,public|private]] (Short form: /linkres)
    ''' </summary>	
    Private m_lstLinkResources As Resources

    ''' <summary>
    ''' /resource:&lt;resinfo&gt;     Adds the specified file as an embedded assembly resource. resinfo:&lt;file&gt;[,&lt;name&gt;[,public|private]] (Short form: /res)
    ''' </summary>
    Private m_lstResources As Resources

    ''' <summary>
    ''' /win32icon:&lt;file&gt;       Specifies a Win32 icon file (.ico) for the default Win32 resources.
    ''' </summary>
    Private m_strWin32Icon As String

    ''' <summary>
    ''' /win32resource:&lt;file&gt;   Specifies a Win32 resource file (.res).
    ''' </summary>
    Private m_strWin32Resource As String

    ' - CODE GENERATION -

    ''' <summary>
    ''' /optimize[+|-]          Enable optimizations.
    ''' </summary>
    Private m_bOptimize As Boolean

    ''' <summary>
    ''' /removeintchecks[+|-]   Remove integer checks. Default off.
    ''' </summary>
    Private m_bRemoveIntChecks As Boolean

    ''' <summary>
    ''' /debug[+|-]             Emit debugging information.
    '''/debug:full             Emit full debugging information (default).
    '''/debug:pdbonly          Emit PDB file only.
    ''' According to #81054 vbc doesn't emit debug info unless /debug is specified.
    ''' </summary>
    Private m_eDebugInfo As DebugTypes = DebugTypes.None

    ' - ERRORS AND WARNINGS -

    ''' <summary>
    ''' /nowarn                 Disable warnings.
    ''' /nowarn:&lt;number_list&gt;   Disable a list of individual warnings.
    ''' </summary>
    Private m_bNoWarn As Boolean
    Private m_NoWarnings As Generic.HashSet(Of Integer)

    ''' <summary>
    ''' /warnaserror[+|-]       Treat warnings as errors.
    ''' </summary>
    Private m_bWarnAsError As Boolean?

    ''' <summary>
    ''' /warnaserror:list       Treat the specified warnings as errors.
    ''' </summary>
    Private m_WarningsAsError As Generic.HashSet(Of Integer)

    ' - LANGUAGE -

    ''' <summary>
    ''' /define:&lt;symbol_list&gt;   Declare global conditional compilation symbol(s). symbol_list:name=value,... (Short form: /d)
    ''' </summary>
    Private m_lstDefine As New Defines

    ''' <summary>
    ''' /imports:&lt;import_list&gt;  Declare global Imports for namespaces in referenced metadata files. import_list:namespace,...
    ''' </summary>
    Private m_lstImports As ImportsStatement

    ''' <summary>
    ''' /optionexplicit[+|-]    Require explicit declaration of variables.
    ''' </summary>
    Private m_eOptionExplicit As OptionExplicitTypes = OptionExplicitTypes.On

    ''' <summary>
    ''' /optionstrict[+|-]      Enforce strict language semantics.
    ''' </summary>
    Private m_eOptionStrict As OptionStrictTypes = OptionStrictTypes.Off

    ''' <summary>
    ''' /optioninfer[+|-]       Allow type inference of variables.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_eOptionInfer As OptionInferTypes = OptionInferTypes.Off

    ''' <summary>
    ''' /rootnamespace:&lt;string&gt; Specifies the root Namespace for all type declarations.
    ''' </summary>
    Private m_strRootNamespace As String

    ''' <summary>
    '''/optioncompare:binary   Specifies binary-style string comparisons. This is the default.
    '''/optioncompare:text     Specifies text-style string comparisons.
    ''' </summary>
    Private m_eOptionCompare As OptionCompareTypes = OptionCompareTypes.Binary

    ' - MISCELLANEOUS -

    ''' <summary>
    ''' /help                   Display this usage message. (Short form: /?)
    ''' </summary>
    Private m_bHelp As Boolean

    ''' <summary>
    ''' /nologo                 Do not display compiler copyright banner.
    ''' </summary>
    Private m_bNoLogo As Boolean

    ''' <summary>
    ''' /quiet                  Quiet output mode. 
    ''' </summary>
    Private m_bQuiet As Boolean

    ''' <summary>
    ''' /verbose                Display verbose messages.
    ''' </summary>
    Private m_bVerbose As Boolean

    ''' <summary>
    ''' /noconfig               Disable the automatic inclusion of the vbnc.rsp response file.
    ''' </summary>
    Private m_bNoConfig As Boolean

    ''' <summary>
    ''' /nostdlib               Do not reference standard libraries (System.dll and vbnc.rsp)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_bNoStdLib As Boolean

    ''' <summary>
    ''' /trace                  Output trace messages (vbnc extension)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_bTrace As Boolean

    ' - ADVANCED -

    ''' <summary>
    ''' /baseaddress:&lt;number&gt;   The base address for a library or module (hex).
    ''' </summary>
    Private m_strBaseAddress As String

    ''' <summary>
    ''' /bugreport:&lt;file&gt;       Create bug report file.
    ''' </summary>
    Private m_strBugReport As String

    ''' <summary>
    ''' /codepage:&lt;number&gt;      Specifies the codepage to use when opening source files.
    ''' </summary>
    Private m_Encoding As System.Text.Encoding

    ''' <summary>
    ''' /delaysign[+|-]         Delay-sign the assembly using only the public portion of the strong name key.
    ''' </summary>
    Private m_bDelaySign As Boolean

    ''' <summary>
    ''' /keycontainer:&lt;string&gt;  Specifies a strong name key container.
    ''' </summary>
    Private m_strKeyContainer As String

    ''' <summary>
    ''' /keyfile:&lt;file&gt;         Specifies a strong name key file.
    ''' </summary>
    Private m_strKeyFile As String

    ''' <summary>
    ''' /libpath:&lt;path_list&gt;    List of directories to search for metadata references. (Semi-colon delimited.)'
    ''' </summary>
    Private m_lstLibPath As New Specialized.StringCollection

    ''' <summary>
    ''' /main:&lt;class&gt;           Specifies tPublic Class ss or Module that contains Sub Main. It can also be a Public Class that inherits from System.Windows.Forms.Form. (Short form: /m)
    ''' </summary>
    Private m_strMain As String

    ''' <summary>
    ''' /netcf                  Target the .NET Compact Framework.
    ''' </summary>
    Private m_bNetCF As Boolean

    ''' <summary>
    ''' /sdkpath:&lt;path&gt;         Location of the .NET Framework SDK directory (mscorlib.dll).
    ''' </summary>
    Private m_strSDKPath As String

    ''' <summary>
    ''' /utf8output[+|-]        Emit compiler output in UTF8 character encoding.
    ''' </summary>
    Private m_bUTF8Output As Boolean

    Private m_VBRuntime As String = "Microsoft.VisualBasic.dll"

    ''' <summary>
    ''' /vbversion:[7|7.1|8]    Which version of the VB language to target. 7 and 7.1 will emit v1.0 assemblies (not supported yet), and 8 will emit v2.0 assemblies. Default is latest (8).
    ''' </summary>
    ''' <remarks></remarks>
    Private m_VBVersion As VBVersions = VBVersions.V8
    ' - OUTPUT FILE -


    ReadOnly Property VBVersion() As VBVersions
        Get
            Return m_VBVersion
        End Get
    End Property

    ''' <summary>
    ''' /out:&lt;file&gt;             Specifies the output file name.
    ''' </summary>
    ReadOnly Property Out() As String
        Get
            Return m_strOut
        End Get
    End Property

    ''' <summary>
    ''' /target:exe             Create a console application (default). (Short form: /t)
    ''' /target:winexe          Create a Windows application.
    ''' /target:library         Create a library assembly.
    ''' /target:module          Create a module that can be added to an assembly.
    ''' </summary>
    ReadOnly Property Target() As Targets
        Get
            Return m_strTarget
        End Get
    End Property

    ' - INPUT FILES - 

    ''' <summary>
    ''' /addmodule:&lt;file&gt;       Reference metadata from the specified module.
    ''' </summary>
    ReadOnly Property Modules() As Specialized.StringCollection
        Get
            Return m_lstModules
        End Get
    End Property

    ''' <summary>
    ''' /recurse:&lt;wildcard&gt;     Include all files in the current directory and subdirectories according to the wildcard specifications.
    ''' </summary>
    ReadOnly Property Recurse() As Specialized.StringCollection
        Get
            Return m_lstRecurse
        End Get
    End Property

    ''' <summary>
    ''' /reference:&lt;file_list&gt;  Reference metadata from the specified assembly. (Short form: /r)
    ''' </summary>
    ReadOnly Property References() As Specialized.StringCollection
        Get
            Return m_lstReferences
        End Get
    End Property

    ' - RESOURCES -

    ''' <summary>
    ''' /linkresource:&lt;resinfo&gt; Links the specified file as an external assembly resource. resinfo:&lt;file&gt;[,&lt;name&gt;[,public|private]] (Short form: /linkres)
    ''' </summary>	
    ReadOnly Property LinkResources() As Resources
        Get
            Return m_lstLinkResources
        End Get
    End Property

    ''' <summary>
    ''' /resource:&lt;resinfo&gt;     Adds the specified file as an embedded assembly resource. resinfo:&lt;file&gt;[,&lt;name&gt;[,public|private]] (Short form: /res)
    ''' </summary>
    ReadOnly Property Resources() As Resources
        Get
            Return m_lstResources
        End Get
    End Property

    ''' <summary>
    ''' /win32icon:&lt;file&gt;       Specifies a Win32 icon file (.ico) for the default Win32 resources.
    ''' </summary>
    ReadOnly Property Win32Icon() As String
        Get
            Return m_strWin32Icon
        End Get
    End Property

    ''' <summary>
    ''' /win32resource:&lt;file&gt;   Specifies a Win32 resource file (.res).
    ''' </summary>
    ReadOnly Property Win32Resource() As String
        Get
            Return m_strWin32Resource
        End Get
    End Property

    ' - CODE GENERATION -

    ''' <summary>
    ''' /optimize[+|-]          Enable optimizations.
    ''' </summary>
    ReadOnly Property Optimize() As Boolean
        Get
            Return m_bOptimize
        End Get
    End Property

    ''' <summary>
    ''' /removeintchecks[+|-]   Remove integer checks. Default off.
    ''' </summary>
    ReadOnly Property RemoveIntChecks() As Boolean
        Get
            Return m_bRemoveIntChecks
        End Get
    End Property

    ''' <summary>
    '''/debug:full             Emit full debugging information (default).
    '''/debug:pdbonly          Emit PDB file only.
    ''' </summary>
    ReadOnly Property DebugInfo() As DebugTypes
        Get
            Return m_eDebugInfo
        End Get
    End Property

    ' - ERRORS AND WARNINGS -

    ''' <summary>
    ''' /nowarn                 Disable warnings.
    ''' </summary>
    ReadOnly Property NoWarn() As Boolean
        Get
            Return m_bNoWarn
        End Get
    End Property

    ''' <summary>
    ''' /nowarn:&lt;number_list&gt;  Disable a list of individual warnings.
    ''' </summary>
    ReadOnly Property NoWarnings As Generic.HashSet(Of Integer)
        Get
            Return m_NoWarnings
        End Get
    End Property

    ''' <summary>
    ''' /warnaserror:list       Treat the specified warnings as errors.
    ''' </summary>
    ReadOnly Property WarningsAsError As Generic.HashSet(Of Integer)
        Get
            Return m_WarningsAsError
        End Get
    End Property

    ''' <summary>
    ''' /warnaserror[+|-]       Treat warnings as errors.
    ''' </summary>
    ReadOnly Property WarnAsError() As Boolean?
        Get
            Return m_bWarnAsError
        End Get
    End Property

    ' - LANGUAGE -

    ''' <summary>
    ''' /define:&lt;symbol_list&gt;   Declare global conditional compilation symbol(s). symbol_list:name=value,... (Short form: /d)
    ''' </summary>
    ReadOnly Property Define() As Defines
        Get
            Return m_lstDefine
        End Get
    End Property

    ''' <summary>
    ''' /imports:&lt;import_list&gt;  Declare global Imports for namespaces in referenced metadata files. import_list:namespace,...
    ''' </summary>
    ReadOnly Property [Imports]() As ImportsStatement
        Get
            Return m_lstImports
        End Get
    End Property

    ''' <summary>
    ''' /optionexplicit[+|-]    Require explicit declaration of variables.
    ''' </summary>
    ReadOnly Property OptionExplicit() As OptionExplicitTypes
        Get
            Return m_eOptionExplicit
        End Get
    End Property

    ''' <summary>
    ''' /optionstrict[+|-]      Enforce strict language semantics.
    ''' </summary>
    ReadOnly Property OptionStrict() As OptionStrictTypes
        Get
            Return m_eOptionStrict
        End Get
    End Property

    ''' <summary>
    ''' /rootnamespace:&lt;string&gt; Specifies the root Namespace for all type declarations.
    ''' </summary>
    ReadOnly Property RootNamespace() As String
        Get
            Return m_strRootNamespace
        End Get
    End Property

    ''' <summary>
    '''/optioncompare:binary   Specifies binary-style string comparisons. This is the default.
    '''/optioncompare:text     Specifies text-style string comparisons.
    ''' </summary>
    ReadOnly Property OptionCompare() As OptionCompareTypes
        Get
            Return m_eOptionCompare
        End Get
    End Property

    ''' <summary>
    ''' /optioninfer[+|-]       Allow type inference of variables.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property OptionInfer As OptionInferTypes
        Get
            Return m_eOptionInfer
        End Get
    End Property

    ' - MISCELLANEOUS -

    ''' <summary>
    ''' /help                   Display this usage message. (Short form: /?)
    ''' </summary>
    ReadOnly Property Help() As Boolean
        Get
            Return m_bHelp
        End Get
    End Property

    ''' <summary>
    ''' /nologo                 Do not display compiler copyright banner.
    ''' </summary>
    ReadOnly Property NoLogo() As Boolean
        Get
            Return m_bNoLogo
        End Get
    End Property

    ''' <summary>
    ''' /quiet                  Quiet output mode. 
    ''' </summary>
    ReadOnly Property Quiet() As Boolean
        Get
            Return m_bQuiet
        End Get
    End Property

    ''' <summary>
    ''' /verbose                Display verbose messages.
    ''' </summary>
    ReadOnly Property Verbose() As Boolean
        Get
            Return m_bVerbose
        End Get
    End Property

    ''' <summary>
    ''' /noconfig               Disable the automatic inclusion of the vbnc.rsp response file.
    ''' </summary>
    ReadOnly Property NoConfig() As Boolean
        Get
            Return m_bNoConfig
        End Get
    End Property

    ''' <summary>
    ''' /nostdlib               Do not reference the standard libraries (vbnc.rsp and System.dll)
    ''' </summary>
    ReadOnly Property NoStdLib As Boolean
        Get
            Return m_bNoStdLib
        End Get
    End Property

    ''' <summary>
    ''' /trace                  Output trace messages (vbnc extension)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Trace As Boolean
        Get
            Return m_bTrace
        End Get
    End Property

    ReadOnly Property VBRuntime() As String
        Get
            Return m_VBRuntime
        End Get
    End Property

    ' - ADVANCED -

    ''' <summary>
    ''' /baseaddress:&lt;number&gt;   The base address for a library or module (hex).
    ''' </summary>
    ReadOnly Property BaseAddress() As String
        Get
            Return m_strBaseAddress
        End Get
    End Property

    ''' <summary>
    ''' /bugreport:&lt;file&gt;       Create bug report file.
    ''' </summary>
    ReadOnly Property BugReport() As String
        Get
            Return m_strBugReport
        End Get
    End Property

    ''' <summary>
    ''' /codepage:&lt;number&gt;      Specifies the codepage to use when opening source files.
    ''' </summary>
    ReadOnly Property Encoding() As System.Text.Encoding
        Get
            If m_Encoding Is Nothing Then m_Encoding = System.Text.Encoding.Default
            Return m_Encoding
        End Get
    End Property

    ''' <summary>
    ''' /delaysign[+|-]         Delay-sign the assembly using only the public portion of the strong name key.
    ''' </summary>
    ReadOnly Property DelaySign() As Boolean
        Get
            Return m_bDelaySign
        End Get
    End Property

    ''' <summary>
    ''' /keycontainer:&lt;string&gt;  Specifies a strong name key container.
    ''' </summary>
    ReadOnly Property KeyContainer() As String
        Get
            Return m_strKeyContainer
        End Get
    End Property

    ''' <summary>
    ''' /keyfile:&lt;file&gt;         Specifies a strong name key file.
    ''' </summary>
    ReadOnly Property KeyFile() As String
        Get
            Return m_strKeyFile
        End Get
    End Property

    ''' <summary>
    ''' /libpath:&lt;path_list&gt;    List of directories to search for metadata references. (Semi-colon delimited.)'
    ''' </summary>
    ReadOnly Property LibPath() As Specialized.StringCollection
        Get
            Return m_lstLibPath
        End Get
    End Property

    ''' <summary>
    ''' /main:&lt;class&gt;           Specifies the Public Class or Module that contains Sub Main. It can also be a Public Class that inherits from System.Windows.Forms.Form. (Short form: /m)
    ''' </summary>
    ReadOnly Property Main() As String
        Get
            Return m_strMain
        End Get
    End Property

    ''' <summary>
    ''' /netcf                  Target the .NET Compact Framework.
    ''' </summary>
    ReadOnly Property NetCF() As Boolean
        Get
            Return m_bNetCF
        End Get
    End Property

    ''' <summary>
    ''' /sdkpath:&lt;path&gt;         Location of the .NET Framework SDK directory (mscorlib.dll).
    ''' </summary>
    ReadOnly Property SDKPath() As String
        Get
            Return m_strSDKPath
        End Get
    End Property

    ''' <summary>
    ''' /utf8output[+|-]        Emit compiler output in UTF8 character encoding.
    ''' </summary>
    ReadOnly Property UTF8Output() As Boolean
        Get
            Return m_bUTF8Output
        End Get
    End Property

    ''' <summary>
    ''' A list of all the response files specified on the command line.
    ''' </summary>
    ReadOnly Property ResponseFiles() As Specialized.StringCollection
        Get
            Return m_lstResponseFiles
        End Get
    End Property

#End Region

    ReadOnly Property AllArgumentsAsArray() As String()
        Get
            Dim result(m_lstAllArgs.Count - 1) As String
            m_lstAllArgs.CopyTo(result, 0)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' This property returns the files the commandline parser found on the commandline,
    ''' this includes expanded wildcards.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Files() As CodeFiles
        Get
            Return m_lstFileNames
        End Get
    End Property

    ''' <summary>
    ''' Create a new commandline parser!
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
        m_lstImports = New ImportsStatement(Compiler)
        m_lstImports.Init(New ImportsClauses(m_lstImports))
        m_lstFileNames = New CodeFiles(m_Compiler)
        m_lstResources = New Resources(m_Compiler, False)
        m_lstLinkResources = New Resources(m_Compiler, True)
    End Sub

    ''' <summary>
    ''' Parse the specified arguments
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Parse(ByVal CommandLine As String()) As Boolean
        Dim result As Boolean = True

        Try
            result = ParseInternal(CommandLine) AndAlso result

            If m_bNoConfig = False AndAlso m_bNoStdLib = False Then
                Dim defaultrspfile As String = Nothing
                Dim compiler_path As String = System.Reflection.Assembly.GetExecutingAssembly.Location
                If compiler_path = String.Empty Then
                    compiler_path = System.Reflection.Assembly.GetEntryAssembly.Location
                End If
                defaultrspfile = IO.Path.Combine(IO.Path.GetDirectoryName(compiler_path), "vbnc.rsp")

                If defaultrspfile Is Nothing OrElse IO.File.Exists(defaultrspfile) = False Then
                    Try
                        Using resources As System.IO.Stream = Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("vbnc.vbnc.rsp")
                            If resources IsNot Nothing Then
                                Using reader As New IO.StreamReader(resources)
                                    Dim tmp As String = reader.ReadToEnd()
                                    IO.File.WriteAllText(defaultrspfile, tmp)
                                End Using
                            End If
                        End Using
                    Catch ex As Exception
                        'Ignore any exceptions here.
                    End Try
                End If
                If IO.File.Exists(defaultrspfile) Then
                    result = ParseResponseFile(defaultrspfile) AndAlso result
                Else
                    Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Default response file '" & defaultrspfile & "' was not loaded because it couldn't be found.")
                End If
            End If
        Catch ex As Exception
            Helper.StopIfDebugging()
            Throw
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Reads the specified response file and sends the arguments to pParse.
    ''' </summary>
    Private Function ParseResponseFile(ByVal Filename As String) As Boolean
        If m_lstResponseFiles.Contains(Filename) Then
            Compiler.Report.SaveMessage(Messages.VBNC2003, Span.CommandLineSpan, IO.Path.GetFullPath(Filename))
            Return False
        Else
            m_lstResponseFiles.Add(Filename)
        End If

        Dim lstArgs As New Specialized.StringCollection
        Dim strLines As String()

        If IO.File.Exists(Filename) Then
            'Do nothing
            Filename = IO.Path.GetFullPath(Filename)
        ElseIf IO.File.Exists(IO.Path.GetFullPath(Filename)) Then
            Filename = IO.Path.GetFullPath(Filename)
        Else
#If DEBUG Then
            Compiler.Report.WriteLine("IO.File.Exists(" & Filename & ") => " & IO.File.Exists(Filename).ToString)
            Compiler.Report.WriteLine("IO.File.Exists(" & IO.Path.GetFullPath(Filename) & ") >= " & IO.File.Exists(IO.Path.GetFullPath(Filename)).ToString)
#End If
            Compiler.Report.ShowMessage(Messages.VBNC2001, Span.CommandLineSpan, Filename)
            Return False
        End If

        Helper.Assert(IO.File.Exists(Filename))

        Try
            strLines = IO.File.ReadAllLines(Filename)
        Catch ex As IO.IOException
            Compiler.Report.ShowMessage(Messages.VBNC2007, Span.CommandLineSpan, Filename)
            Return False
        End Try

        'Read the file, line by line.
        For Each strLine As String In strLines
            If strLine.StartsWith("#") = False Then 'Skip comment lines
                lstArgs.AddRange(Helper.ParseLine(strLine))    'Add the parsed elements of the line
            End If
        Next

        'Create a string array from the arraylist
        Dim strArgs(lstArgs.Count - 1) As String
        lstArgs.CopyTo(strArgs, 0)
        'Parse the arguments
        Return ParseInternal(strArgs)
    End Function

    ''' <summary>
    ''' Add all the files corresponding to the specified pattern in the specified
    ''' directory to the list of code files, recursively.
    ''' </summary>
    Private Function AddFilesInDir(ByVal dir As String, ByVal relativepath As String, ByVal pattern As String) As Boolean
        Dim strFiles() As String
        Dim result As Boolean = True

        strFiles = IO.Directory.GetFiles(dir, pattern)
        For Each strFile As String In strFiles
            m_lstFileNames.Add(New CodeFile(strFile, relativepath, Me.Compiler))
        Next
        result = True
        strFiles = IO.Directory.GetDirectories(dir)
        For Each strDir As String In strFiles
            result = AddFilesInDir(strDir, IO.Path.Combine(relativepath, System.IO.Path.GetFileName(strDir)), pattern) AndAlso result
        Next
        'whoami()
        Return result
    End Function

    Private Function SetOption(ByVal strName As String, ByVal strValue As String) As Boolean
        Dim result As Boolean = True
        Select Case LCase(strName)
            ' - OUTPUT FILE -
            Case "out"
                m_strOut = strValue
            Case "target", "t"
                Select Case LCase(strValue)
                    Case "exe"
                        m_strTarget = Targets.Console
                    Case "winexe"
                        m_strTarget = Targets.Winexe
                    Case "library"
                        m_strTarget = Targets.Library
                    Case "module"
                        m_strTarget = Targets.Module
                    Case Else
                        Compiler.Report.SaveMessage(Messages.VBNC2019, Span.CommandLineSpan, "target", strValue)
                        result = False
                End Select
                ' - INPUT FILES -
            Case "addmodule"
                m_lstModules.AddRange(Split(strValue, ","))
            Case "recurse"
                'Add the files
                Dim strPath As String = System.IO.Path.GetDirectoryName(strValue)
                Dim strRelativePath As String = strPath
                Dim strFileName As String
                If strPath <> "" Then
                    strFileName = strValue.Substring(strPath.Length + 1)
                Else
                    strPath = IO.Directory.GetCurrentDirectory()
                    strFileName = strValue
                End If
                result = AddFilesInDir(strPath, strRelativePath, strFileName) AndAlso result
            Case "reference", "r"
                m_lstReferences.AddRange(Split(strValue, ","))
                ' - RESOURCES -
            Case "linkresource", "linkres"
                result = m_lstLinkResources.Add(strValue) AndAlso result
            Case "resource", "res"
                result = m_lstResources.Add(strValue) AndAlso result
            Case "win32icon"
                m_strWin32Icon = strValue
            Case "win32resource"
                m_strWin32Resource = strValue
                ' - CODE GENERATION -
            Case "optimize+", "optimize"
                m_bOptimize = True
            Case "optimize-"
                m_bOptimize = False
            Case "removeintchecks+", "removeintchecks"
                m_bRemoveIntChecks = True
            Case "removeintchecks-"
                m_bRemoveIntChecks = False
            Case "debug+"
                m_eDebugInfo = DebugTypes.Full
            Case "debug-"
                m_eDebugInfo = DebugTypes.None
            Case "debug"
                Select Case LCase(strValue)
                    Case "full"
                        m_eDebugInfo = DebugTypes.Full
                    Case "pdbonly"
                        m_eDebugInfo = DebugTypes.PDB
                    Case ""
                        m_eDebugInfo = DebugTypes.Full
                    Case Else
                        'TODO: AddError 2014 (saved).
                        Compiler.Report.SaveMessage(Messages.VBNC2019, Span.CommandLineSpan, strName, strValue)
                        result = False
                End Select
                ' - ERRORS AND WARNINGS -
            Case "nowarn"
                If strValue <> String.Empty Then
                    For Each number As String In strValue.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                        Dim n As Integer
                        If Integer.TryParse(number, Globalization.NumberStyles.AllowLeadingWhite Or Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, n) Then
                            If m_NoWarnings Is Nothing Then m_NoWarnings = New Generic.HashSet(Of Integer)
                            m_NoWarnings.Add(n)
                        Else
                            Compiler.Report.SaveMessage(Messages.VBNC2014, Span.CommandLineSpan, number, "nowarn")
                        End If
                    Next
                End If
                m_bNoWarn = True
            Case "warnaserror+", "warnaserror"
                If strValue <> String.Empty Then
                    For Each number As String In strValue.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                        Dim n As Integer
                        If Integer.TryParse(number, Globalization.NumberStyles.AllowLeadingWhite Or Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, n) Then
                            If m_WarningsAsError Is Nothing Then m_WarningsAsError = New Generic.HashSet(Of Integer)
                            m_WarningsAsError.Add(n)
                        Else
                            Compiler.Report.SaveMessage(Messages.VBNC2014, Span.CommandLineSpan, number, "warnaserror")
                        End If
                    Next
                Else
                    m_bWarnAsError = True
                End If
            Case "warnaserror-"
                If strValue <> String.Empty Then
                    If m_WarningsAsError IsNot Nothing Then
                        For Each number As String In strValue.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            Dim n As Integer
                            If Integer.TryParse(number, Globalization.NumberStyles.AllowLeadingWhite Or Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, n) Then
                                m_WarningsAsError.Remove(n)
                            Else
                                Compiler.Report.SaveMessage(Messages.VBNC2014, Span.CommandLineSpan, number, "warnaserror")
                            End If
                        Next
                    End If
                Else
                    m_bWarnAsError = False
                End If
                ' - LANGUAGE -
            Case "define", "d"
                'FIXME: This does not work with commas inside strings.
                Dim strDefines() As String = Split(strValue, ",")
                For Each str As String In strDefines
                    If str.Contains("=") = False Then str = str & "=True"
                    Dim strSplit() As String = Split(str, "=")
                    If strSplit.GetUpperBound(0) <> 1 Then
                        Compiler.Report.ShowMessage(Messages.VBNC90017, Span.CommandLineSpan, str)
                        result = False
                    Else
                        m_lstDefine.Add(New Define(Compiler, strSplit(0), strSplit(1)))
                    End If
                Next
            Case "imports"
                Dim imps As String()
                imps = strValue.Split(","c)
                For Each str As String In imps
                    If str <> "" Then
                        result = vbnc.Parser.ParseImportsStatement(m_lstImports, str) AndAlso result
                    End If
                Next
            Case "optionexplicit+", "optionexplicit"
                m_eOptionExplicit = OptionExplicitTypes.On
            Case "optionexplicit-"
                m_eOptionExplicit = OptionExplicitTypes.Off
            Case "optionstrict+"
                m_eOptionStrict = OptionStrictTypes.On
            Case "optionstrict"
                If strValue = "custom" Then
                    m_eOptionStrict = OptionStrictTypes.Off
                Else
                    m_eOptionStrict = OptionStrictTypes.On
                End If
            Case "optionstrict-"
                m_eOptionStrict = OptionStrictTypes.Off
            Case "optioninfer+", "optioninfer"
                m_eOptionInfer = OptionInferTypes.On
            Case "optioninfer-"
                m_eOptionInfer = OptionInferTypes.Off
            Case "rootnamespace"
                m_strRootNamespace = strValue
            Case "optioncompare"
                Select Case LCase(strValue)
                    Case "text"
                        m_eOptionCompare = OptionCompareTypes.Text
                    Case "binary"
                        m_eOptionCompare = OptionCompareTypes.Binary
                    Case Else
                        result = False
                        'TODO: AddError 2014 (saved).
                        Compiler.Report.SaveMessage(Messages.VBNC2019, Span.CommandLineSpan, strName, strValue)
                End Select
                ' - MISCELLANEOUS -
            Case "help", "?"
                m_bHelp = True
            Case "nologo"
                m_bNoLogo = True
            Case "quiet"
                m_bQuiet = True
            Case "verbose", "verbose+"
                m_bVerbose = True
            Case "verbose-"
                m_bVerbose = False
            Case "noconfig"
                m_bNoConfig = True
            Case "nostdlib"
                m_bNoStdLib = True
            Case "trace"
                m_bTrace = True
                ' - ADVANCED -
            Case "baseaddress"
                m_strBaseAddress = strValue
            Case "bugreport"
                m_strBugReport = strValue
            Case "codepage"
                If strValue = "" Then
                    Compiler.Report.ShowMessage(Messages.VBNC2006, Span.CommandLineSpan, "codepage", "<number>")
                    result = False
                Else
                    Try
                        m_Encoding = System.Text.Encoding.GetEncoding(Integer.Parse(strValue, Globalization.NumberStyles.AllowLeadingWhite Or Globalization.NumberStyles.AllowLeadingSign, System.Globalization.CultureInfo.InvariantCulture))
                    Catch
                        Compiler.Report.ShowMessage(Messages.VBNC2016, Span.CommandLineSpan, strValue)
                        result = False
                    End Try
                End If
            Case "delaysign+"
                m_bDelaySign = True
            Case "delaysign-"
                m_bDelaySign = False
            Case "keycontainer"
                m_strKeyContainer = strValue
            Case "keyfile"
                Dim paths() As String
                paths = Me.GetFullPaths(strValue)
                If paths IsNot Nothing AndAlso paths.Length = 1 Then
                    m_strKeyFile = paths(0)
                Else
                    Helper.AddError(Compiler, """")
                End If
            Case "libpath"
                m_lstLibPath.AddRange(Split(strValue, ","))
            Case "main"
                m_strMain = strValue
            Case "netcf"
                m_bNetCF = True
                result = False
                Compiler.Report.ShowMessage(Messages.VBNC90016, Span.CommandLineSpan, ".NET Compact Framework")
            Case "sdkpath"
                m_strSDKPath = strValue
            Case "utf8output+", "utf8output"
                m_bUTF8Output = True
            Case "utf8output-"
                m_bUTF8Output = False
            Case "novbruntimeref"
                m_VBRuntime = Nothing
            Case "vbruntime-"
                m_VBRuntime = Nothing
            Case "vbruntime+"
                m_VBRuntime = "Microsoft.VisualBasic.dll"
            Case "vbruntime"
                m_VBRuntime = strValue
            Case "errorreport"
                result = Compiler.Report.SaveMessage(Messages.VBNC99998, Span.CommandLineSpan, "/errorreport isn't implemented yet.") AndAlso result
            Case "vbversion"
                Select Case strValue
                    Case "7"
                        m_VBVersion = VBVersions.V7
                    Case "7.1"
                        m_VBVersion = VBVersions.V7_1
                    Case "8"
                        m_VBVersion = VBVersions.V8
                    Case Else
                        Helper.AddWarning("Unknown vb version: " & strValue & ", will use default vbversion (8)")
                End Select
            Case "doc-"
                m_strDoc = Nothing
            Case "doc+"
                m_strDoc = String.Empty
                Compiler.Report.SaveMessage(Messages.VBNC99998, Span.CommandLineSpan, "Support for /doc+ has not been implemented. No documentation file will be generated.")
            Case "doc"
                m_strDoc = strValue
                Compiler.Report.SaveMessage(Messages.VBNC99998, Span.CommandLineSpan, "Support for /doc:<file> has not been implemented. No documentation file will be generated.")
            Case Else
                'result = False 'OK since this is only a warning.
                result = Compiler.Report.SaveMessage(Messages.VBNC2009, Span.CommandLineSpan, strName) AndAlso result
        End Select
        Return result
    End Function

    Private Function AddFile(ByVal File As String) As Boolean
        Dim result As Boolean = True

        Dim strFile As String
        Dim strFiles As String()

        strFiles = GetFullPaths(File)

        If strFiles Is Nothing OrElse strFiles.Length = 0 Then
            If IsPattern(File) = False Then
                result = Compiler.Report.SaveMessage(Messages.VBNC2001, Span.CommandLineSpan, File) AndAlso result
            End If
            Return result
        End If

        For Each strFile In strFiles
            m_lstFileNames.Add(New CodeFile(strFile, System.IO.Path.GetDirectoryName(File), Me.Compiler))
        Next

        Return result
    End Function

    ''' <summary>
    ''' Parses the commandline
    ''' Returns false if no commandline found, or if there was an error parsing the commandline.
    ''' Messages here are saved, since they have to be shown after the logo is shown, but the 
    ''' showing of the logo can be cancelled on the commandline, so the entire commandline
    ''' has to the parsed before any messages are shown.
    ''' </summary>
    Private Function ParseInternal(ByVal Args() As String) As Boolean
        Dim result As Boolean = True
        m_lstAllArgs.AddRange(Args)
        For Each s As String In Args
            If s.StartsWith("@") Then
                result = ParseResponseFile(s.Substring(1)) AndAlso result
                Continue For
            End If

            Dim isOption As Boolean

            isOption = s.StartsWith("-"c)
            If isOption = False AndAlso s.StartsWith("/"c) Then
                Dim idxSecond As Integer = s.IndexOf("/"c, 2)
                Dim idxColon As Integer = s.IndexOf(":"c, 2)

                isOption = idxColon >= 0 OrElse idxSecond = -1
            End If

            If isOption Then
                Dim strName As String = "", strValue As String = ""
                'Find the colon which separates the values
                Dim iColon As Integer = InStr(2, s, ":", CompareMethod.Binary)
                'If found, split it in a name and value pair
                If iColon > 0 Then
                    strName = s.Substring(1, iColon - 2)
                    strValue = s.Substring(strName.Length + 2)
                Else
                    'if not, the whole string is the name
                    strName = s.Substring(1)
                End If
                'find the option
                result = SetOption(strName, strValue) AndAlso result
                Continue For
            End If

            AddFile(s)
        Next

        Return result
    End Function

    ''' <summary>
    ''' Shows an error if the filename(s) cannot be found.
    ''' If it returns an empty array something went wrong 
    ''' (the error message has already been shown).
    ''' </summary>
    ''' <param name="FileName">Can be a complete filename or a pattern.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFullPaths(ByVal FileName As String) As String()
        Dim strPath As String = System.IO.Path.GetDirectoryName(FileName)
        Dim strFileName As String

        If strPath <> "" Then
            strFileName = FileName.Substring(strPath.Length + 1)
        Else
            strFileName = FileName
        End If

        Dim tmpPath As String
        If strPath = "" Then
            strPath = IO.Path.GetDirectoryName(IO.Path.GetFullPath(FileName))
        End If
        tmpPath = IO.Path.GetFullPath(strPath)

        If IO.Directory.Exists(tmpPath) = False Then Return Nothing

        If IsPattern(FileName) Then
            Return IO.Directory.GetFiles(tmpPath, strFileName)
        Else
            Dim file As String = IO.Path.Combine(tmpPath, strFileName)
            If IO.File.Exists(file) Then
                Return New String() {file}
            Else
                Return Nothing
            End If
        End If
    End Function

    Shared Function IsPattern(ByVal Filename As String) As Boolean
        Return Filename.IndexOfAny(PATTERNCHARS) >= 0
    End Function

    ''' <summary>
    ''' Returns a collection of all the arguments parsed (included in 
    ''' response files).
    ''' </summary>
    ReadOnly Property AllArguments() As Specialized.StringCollection
        Get
            Return m_lstAllArgs
        End Get
    End Property
End Class


Partial Public Class CommandLine
    Public Enum VBVersions
        V7
        V7_1
        V8
    End Enum
End Class

