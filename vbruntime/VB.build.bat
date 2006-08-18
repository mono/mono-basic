echo off
echo ====================================
echo = 	Batch build Microsoft.VisualBasic.dll
echo =	
echo =	sample usage: build VB using .NET 1.1 as debug
echo =	VB.build.bat 1 debug
echo =	
echo =	sample use: build VB using .NET 2.0 as release
echo =	VB.build.bat 2 release
echo =  
echo =
echo ====================================

echo Get batch command parameters.
SET VB_BUILD_PARAM_NET_VERSION="%1"
SET VB_BUILD_PARAM_CONFIGURATION="%2"

echo Set command parameters default.
IF %VB_BUILD_PARAM_NET_VERSION%=="" SET VB_BUILD_PARAM_NET_VERSION=1
IF %VB_BUILD_PARAM_CONFIGURATION%=="" SET VB_BUILD_PARAM_CONFIGURATION=debug

echo Set .NET SDK env.
IF %VB_BUILD_PARAM_NET_VERSION%=="1" (
IF NOT DEFINED VSINSTALLDIR call "%VS71COMNTOOLS%vsvars32.bat"
)
IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
IF NOT DEFINED VSINSTALLDIR call "%VS80COMNTOOLS%vsvars32.bat"
)

echo Set VB compile options.
SET VB_COMPILE_OPTIONS=
IF %VB_BUILD_PARAM_NET_VERSION%=="1" ( 
SET VB_COMPILE_OPTIONS=/define:DEBUG=False
IF %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% /debug:full /define:DEBUG=True,TRACE=True 
)

IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
SET VB_COMPILE_OPTIONS=
SET VB_COMPILE_OPTIONS=/nowarn:42016,41999,42017,42018,42019,42032,42036,42020,42021,42022,40005 /errorreport:prompt /noconfig 
SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% /define:DEBUG=False,NET_2_0=True,_MYTYPE=\"Empty\"
IF %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% /debug:full /define:DEBUG=True,TRACE=True,NET_2_0=True,_MYTYPE=\"Empty\" /errorreport:prompt  -verbose
)
echo %VB_COMPILE_OPTIONS%

SET VB_SOURCES=AssemblyInfo.vb ^
Microsoft.VisualBasic\AppWinStyle.vb ^
Microsoft.VisualBasic\AudioPlayMode.vb ^
Microsoft.VisualBasic\CallType.vb ^
Microsoft.VisualBasic\Collection.vb ^
Microsoft.VisualBasic\ComClassAttribute.vb ^
Microsoft.VisualBasic\CompareMethod.vb ^
Microsoft.VisualBasic\Constants.vb ^
Microsoft.VisualBasic\ControlChars.vb ^
Microsoft.VisualBasic\Conversion.vb ^
Microsoft.VisualBasic\DateAndTime.vb ^
Microsoft.VisualBasic\DateFormat.vb ^
Microsoft.VisualBasic\DateInterval.vb ^
Microsoft.VisualBasic\DueDate.vb ^
Microsoft.VisualBasic\ErrObject.vb ^
Microsoft.VisualBasic\FileAttribute.vb ^
Microsoft.VisualBasic\FileSystem.vb ^
Microsoft.VisualBasic\Financial.vb ^
Microsoft.VisualBasic\HideModuleNameAttribute.vb ^
Microsoft.VisualBasic\Information.vb ^
Microsoft.VisualBasic\Interaction.vb ^
Microsoft.VisualBasic\FirstDayOfWeek.vb ^
Microsoft.VisualBasic\FirstWeekOfYear.vb ^
Microsoft.VisualBasic\MsgBoxResult.vb ^
Microsoft.VisualBasic\MsgBoxStyle.vb ^
Microsoft.VisualBasic\MyGroupCollectionAttribute.vb ^
Microsoft.VisualBasic\OpenAccess.vb ^
Microsoft.VisualBasic\OpenMode.vb ^
Microsoft.VisualBasic\OpenShare.vb ^
Microsoft.VisualBasic\SpcInfo.vb ^
Microsoft.VisualBasic\Strings.vb ^
Microsoft.VisualBasic\TabInfo.vb ^
Microsoft.VisualBasic\TriState.vb ^
Microsoft.VisualBasic\VariantType.vb ^
Microsoft.VisualBasic\VBFixedArrayAttribute.vb ^
Microsoft.VisualBasic\VBFixedStringAttribute.vb ^
Microsoft.VisualBasic\VBMath.vb ^
Microsoft.VisualBasic\VbStrConv.vb ^
Microsoft.VisualBasic.CompilerServices\BooleanType.vb ^
Microsoft.VisualBasic.CompilerServices\ByteType.vb ^
Microsoft.VisualBasic.CompilerServices\CharArrayType.vb ^
Microsoft.VisualBasic.CompilerServices\CharType.vb ^
Microsoft.VisualBasic.CompilerServices\Conversions.vb ^
Microsoft.VisualBasic.CompilerServices\DateType.vb ^
Microsoft.VisualBasic.CompilerServices\DecimalType.vb ^
Microsoft.VisualBasic.CompilerServices\DesignerGeneratedAttribute.vb ^
Microsoft.VisualBasic.CompilerServices\DoubleType.vb ^
Microsoft.VisualBasic.CompilerServices\ExceptionUtils.vb ^
Microsoft.VisualBasic.CompilerServices\FlowControl.vb ^
Microsoft.VisualBasic.CompilerServices\HostServices.vb ^
Microsoft.VisualBasic.CompilerServices\IncompleteInitialization.vb ^
Microsoft.VisualBasic.CompilerServices\IntegerType.vb ^
Microsoft.VisualBasic.CompilerServices\InternalErrorException.vb ^
Microsoft.VisualBasic.CompilerServices\IVbHost.vb ^
Microsoft.VisualBasic.CompilerServices\LateBinder.vb ^
Microsoft.VisualBasic.CompilerServices\LateBinding.vb ^
Microsoft.VisualBasic.CompilerServices\LikeOperator.vb ^
Microsoft.VisualBasic.CompilerServices\LongType.vb ^
Microsoft.VisualBasic.CompilerServices\NewLateBinding.vb ^
Microsoft.VisualBasic.CompilerServices\ObjectFlowControl.vb ^
Microsoft.VisualBasic.CompilerServices\ObjectType.vb ^
Microsoft.VisualBasic.CompilerServices\Operators.vb ^
Microsoft.VisualBasic.CompilerServices\OptionCompareAttribute.vb ^
Microsoft.VisualBasic.CompilerServices\OptionTextAttribute.vb ^
Microsoft.VisualBasic.CompilerServices\ProjectData.vb ^
Microsoft.VisualBasic.CompilerServices\ShortType.vb ^
Microsoft.VisualBasic.CompilerServices\SingleType.vb ^
Microsoft.VisualBasic.CompilerServices\StandardModuleAttribute.vb ^
Microsoft.VisualBasic.CompilerServices\StaticLocalInitFlag.vb ^
Microsoft.VisualBasic.CompilerServices\StringType.vb ^
Microsoft.VisualBasic.CompilerServices\Utils.vb ^
Microsoft.VisualBasic.CompilerServices\Versioned.vb ^
Microsoft.VisualBasic.ApplicationServices\ApplicationBase.vb ^
Microsoft.VisualBasic.ApplicationServices\AuthenticationMode.vb ^
Microsoft.VisualBasic.ApplicationServices\ConsoleApplicationBase.vb ^
Microsoft.VisualBasic.ApplicationServices\ShutdownMode.vb ^
Microsoft.VisualBasic.ApplicationServices\User.vb ^
Microsoft.VisualBasic.ApplicationServices\WindowsFormsApplicationBase.vb ^
Microsoft.VisualBasic.Devices\Computer.vb

IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
SET VB_SOURCES=%VB_SOURCES% "My Project\Application.Designer.vb" "My Project\Resources.Designer.vb" "My Project\Settings.Designer.vb"
)

echo compiling ...
pushd Microsoft.VisualBasic
resgen strings.txt
rem TODO: replace vbc with C:\cygwin\monobuild\vbnc\vbnc\bin\vbnc.exe 
echo on
vbc -target:library -optionstrict+ -out:..\bin\Microsoft.VisualBasic.dll -novbruntimeref %VB_COMPILE_OPTIONS% -r:mscorlib.dll -r:System.dll -r:System.Windows.Forms.dll -keyfile:msfinal.pub /res:strings.resources %VB_SOURCES%
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

:FINALLY
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
echo ========================
popd
PAUSE

:END
echo build exceuted using .NET %FRAMEWORKVERSION%
popd
