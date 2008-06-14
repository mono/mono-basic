rem echo off
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
echo Received parameters %1 %2
SET VB_BUILD_PARAM_NET_VERSION="%1"
SET VB_BUILD_PARAM_CONFIGURATION="%2"

echo Set command parameters default.
IF %VB_BUILD_PARAM_NET_VERSION%=="" SET VB_BUILD_PARAM_NET_VERSION="2"
IF %VB_BUILD_PARAM_CONFIGURATION%=="" SET VB_BUILD_PARAM_CONFIGURATION=debug

echo Set .NET SDK env.
IF %VB_BUILD_PARAM_NET_VERSION%=="1" (
IF NOT DEFINED VSINSTALLDIR call "%VS71COMNTOOLS%vsvars32.bat"
)
IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
IF NOT DEFINED VSINSTALLDIR call "%VS80COMNTOOLS%vsvars32.bat"
)

echo Set VB compile options.
rem The option /errorreport:prompt is used to alert the vbc compiler to prompt the reason of a failure.

SET VB_COMPILE_OPTIONS=
IF %VB_BUILD_PARAM_NET_VERSION%=="1" ( 
GOTO SETOPTIONS1
)
IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
GOTO SETOPTIONS2
)
GOTO ENDSETOPTIONS

:SETOPTIONS2
SET VB_COMPILE_OPTIONS=/nowarn:42016,41999,42017,42018,42019,42032,42036,42020,42021,42022,40005 /errorreport:prompt /noconfig /imports:System.Collections,System.Diagnostics,System.Collections.Generic,System
IF NOT %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% /define:DEBUG=False,NET_2_0=True,_MYTYPE=\"Empty\",NET_VER=2.0
IF %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% /debug:full /define:DEBUG=True,TRACE=False,NET_2_0=True,NET_VER=2.0,_MYTYPE=\"Empty\" /errorreport:prompt  -verbose
GOTO ENDSETOPTIONS

:SETOPTIONS1
IF NOT %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=/define:DEBUG=False
IF %VB_BUILD_PARAM_CONFIGURATION%=="debug" SET VB_COMPILE_OPTIONS=/debug:full /define:DEBUG=True,TRACE=True 
GOTO ENDSETOPTIONS

:ENDSETOPTIONS

SET VB_COMPILE_OPTIONS=%VB_COMPILE_OPTIONS% %VB_EXTERNAL_OPTIONS%
echo %VB_COMPILE_OPTIONS%

echo Set VB compile references
SET VB_COMPILE_REFERENCES=
SET VB_COMPILE_REFERENCES=-r:mscorlib.dll -r:System.dll -r:System.Windows.Forms.dll -r:System.Drawing.dll %VB_COMPILE_REFERENCES_J2EE%

SET VB_SOURCES=@Microsoft.VisualBasic.dll.sources.win

echo Set log file options.
set startDate=%date%
set startTime=%time%
set sdy=%startDate:~10%
set /a sdm=1%startDate:~4,2% - 100
set /a sdd=1%startDate:~7,2% - 100
set /a sth=%startTime:~0,2%
set /a stm=1%startTime:~3,2% - 100
set /a sts=1%startTime:~6,2% - 100
set TIMESTAMP=%sdy%_%sdm%_%sdd%_%sth%_%stm%

set OUTPUT_FILE_PREFIX=Microsoft_VisualBasic
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%
set BUILD_LOG=%COMMON_PREFIX%.build.log

echo compiling ...
pushd Microsoft.VisualBasic
resgen strings.txt
rem TODO: replace vbc with C:\cygwin\monobuild\vbnc\vbnc\bin\vbnc.exe 
echo on
vbc -target:library -optionstrict+ -out:..\bin\Microsoft.VisualBasic.dll -novbruntimeref %VB_COMPILE_OPTIONS% %VB_COMPILE_OPTIONS_J2EE% %VB_COMPILE_REFERENCES% /res:strings.resources %VB_SOURCES% >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

sn -q -R ..\bin\Microsoft.VisualBasic.dll ..\..\class\mono.snk

:FINALLY
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
echo ========================
popd

:END
echo build executed using .NET %FRAMEWORKVERSION%
popd
