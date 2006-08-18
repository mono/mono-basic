echo off
echo ====================================
echo = 	Replace Microsoft.VisualBasic.dll at the GAC
echo =	
echo =	sample usage: replace VB using .NET 1.1
echo =	VB.replace.bat 1
echo =	
echo ====================================

echo Get batch command parameters.
SET VB_BUILD_PARAM_NET_VERSION="%1"

echo Set command parameters default.
IF %VB_BUILD_PARAM_NET_VERSION%=="" SET VB_BUILD_PARAM_NET_VERSION=1

echo Set .NET SDK env.
IF %VB_BUILD_PARAM_NET_VERSION%=="1" (
IF NOT DEFINED VSINSTALLDIR call "%VS71COMNTOOLS%vsvars32.bat"
)
IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
IF NOT DEFINED VSINSTALLDIR call "%VS80COMNTOOLS%vsvars32.bat"
)
SET NET_FRAMEWORK_PATH=%FRAMEWORKDIR%\%FRAMEWORKVERSION%\

pushd bin
sn -Vr Microsoft.VisualBasic.dll
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

%NET_FRAMEWORK_PATH%gacutil -i Microsoft.VisualBasic.dll -f
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
popd

IF NOT EXIST %NET_FRAMEWORK_PATH%Microsoft.VisualBasic.orig ren %NET_FRAMEWORK_PATH%Microsoft.VisualBasic.dll Microsoft.VisualBasic.orig
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

copy bin\Microsoft.VisualBasic.dll %NET_FRAMEWORK_PATH%Microsoft.VisualBasic.dll
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

:FINALLY
echo ======================
echo finished
echo ======================
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
echo ========================
PAUSE

:END
