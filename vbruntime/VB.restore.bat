echo off
echo ====================================
echo = 	Restore Microsoft.VisualBasic.dll at the GAC
echo =	
echo =	sample usage: restore VB using .NET 1.1
echo =	VB.restore.bat 1
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

pushd %NET_FRAMEWORK_PATH%
del Microsoft.VisualBasic.dll
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

ren Microsoft.VisualBasic.orig Microsoft.VisualBasic.dll
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

gacutil -i Microsoft.VisualBasic.dll -f
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
popd

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
