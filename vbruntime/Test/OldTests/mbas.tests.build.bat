echo ====================================
echo = 	Batch build VisualBasic mbas tests
echo =	
echo =	sample usage: build VB standalone tests using .NET 1.1
echo =	mbas.tests.build.bat 1
echo =	
echo =	sample use: build VB standalone tests using .NET 2.0
echo =	mbas.tests.build.bat 2
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
DIR mbas\Test\tests\*.vb /s /b >mbas.tests.build.files.tmp
for /f %%f in (mbas.tests.build.files.tmp) do vbc -target:exe -out:%%~pf%%~nf.exe -debug:full -r:mscorlib.dll -r:System.dll -r:Microsoft.VisualBasic.dll %%f

:FINALLY
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
echo ========================
PAUSE

:END
echo build exceuted using .NET %FRAMEWORKVERSION%

