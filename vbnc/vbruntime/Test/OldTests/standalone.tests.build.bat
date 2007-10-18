echo ====================================
echo = 	Batch build VisualBasic standalone tests
echo =	
echo =	sample usage: build VB standalone tests using .NET 1.1
echo =	standalone.tests.build.bat 1
echo =	
echo =	sample use: build VB standalone tests using .NET 2.0
echo =	standalone.tests.build.bat 2
echo =  
echo ====================================
echo off
echo Get batch command parameters.
SET VB_BUILD_PARAM_NET_VERSION="%1"

echo Set command parameters default.
IF %VB_BUILD_PARAM_NET_VERSION%=="" SET VB_BUILD_PARAM_NET_VERSION="2"

echo Set .NET SDK env.
IF %VB_BUILD_PARAM_NET_VERSION%=="1" (
IF NOT DEFINED VSINSTALLDIR call "%VS71COMNTOOLS%vsvars32.bat"
)
IF %VB_BUILD_PARAM_NET_VERSION%=="2" (
IF NOT DEFINED VSINSTALLDIR call "%VS80COMNTOOLS%vsvars32.bat"
)

echo on
DIR standalone\*.vb /s /b >standalone.tests.build.files.tmp
for /f %%f in (standalone.tests.build.files.tmp) do vbc -target:exe -out:%%~pf%%~nf.exe -debug:full -r:mscorlib.dll -r:System.dll -r:Microsoft.VisualBasic.dll %%f standalone\MainModule.vb

:FINALLY
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
echo ========================
PAUSE

:END
echo build exceuted using .NET %FRAMEWORKVERSION%

