ECHO OFF
cls
pushd .
SETLOCAL

IF "%FXVERSION%"=="" (SET FXVERSION=%1)

CALL ..\InitPaths.cmd 
rem ECHO ON
IF NOT "%ERRORLEVEL%"=="0" GOTO ENDOFFILE

rem 
rem Build the vbruntime first without signing it, this way it will be linked to the test assembly and we're executing against our vbruntime
rem Then build it signed, so that we can test against the MS vbruntime.
rem 
cd ..
SET EXTRA_VBRUNTIME_FLAGS=/define:DONTSIGN=true
CALL Make.cmd
IF NOT "%COMPILATIONERROR%"=="" GOTO ENDOFFILE
copy Microsoft.VisualBasic\Microsoft.VisualBasic.dll Test\Microsoft.VisualBasic.NOTSIGNED.dll
SET EXTRA_VBRUNTIME_FLAGS=
CALL Make.cmd
IF NOT "%COMPILATIONERROR%"=="" GOTO ENDOFFILE
copy Microsoft.VisualBasic\Microsoft.VisualBasic.dll Test\Microsoft.VisualBasic.SIGNED.dll
popd

IF %ERRORLEVEL%==1 GOTO ENDOFFILE


copy Microsoft.VisualBasic.SIGNED.dll Microsoft.VisualBasic.dll
CALL :EXECUTETEST
IF NOT "%ERRORLEVEL%"=="0" GOTO ENDOFFILE
copy Microsoft.VisualBasic.NOTSIGNED.dll Microsoft.VisualBasic.dll
CALL :EXECUTETEST
rem del Microsoft.VisualBasic.*.dll
rem del Microsoft.VisualBasic.dll

GOTO ENDOFFILE

:EXECUTETEST
pushd .
copy Microsoft.VisualBasic.dll bin

IF "%FXVERSION%"=="2"   (GOTO SETDEFINES2_0)
IF "%FXVERSION%"=="2_0" (GOTO SETDEFINES2_0)
IF "%FXVERSION%"=="1_1" (GOTO SETDEFINES1_1)
IF "%FXVERSION%"=="1"   (GOTO SETDEFINES1_1)
IF "%FXVERSION%"=="1_0" (GOTO SETDEFINES1_0)
GOTO ENDSETDEFINES
:SETDEFINES2_0
	SET CSDEFINES=-define:NET_1_0,NET_1_1,NET_2_0
	GOTO ENDSETDEFINES
:SETDEFINES1_1
	SET CSDEFINES=-define:NET_1_0,NET_1_1 -debug-
	GOTO ENDSETDEFINES
:SETDEFINES1_0
	SET CSDEFINES=-define:NET_1_0 -debug-
	GOTO ENDSETDEFINES
:ENDSETDEFINES

%CSC% "-out:bin\2005VB_test_CS.dll" @2005VB_test_CS.dll.rsp @2005VB_test_CS.dll.sources.win -lib:bin %CSDEFINES%

IF ERRORLEVEL 1 (GOTO ENDOFFILE)
cd bin

IF "%FXVERSION%"=="2" GOTO SETNUNIT2
IF "%FXVERSION%"=="2_0" GOTO SETNUNIT2
GOTO SETNUNIT1
:SETNUNIT1
copy nunit-console1.exe.config nunit-console.exe.config
GOTO ENDSETNUNITV
:SETNUNIT2
copy nunit-console2.exe.config nunit-console.exe.config
GOTO ENDSETNUNITV
:ENDSETNUNITV

nunit-console.exe 2005VB_test_CS.dll /labels /noshadow /exclude:Slow,NotDotNet,NotWorking
ECHO %ERRORLEVEL%
IF NOT "%ERRORLEVEL%"=="0" GOTO ENDOFFILE
rem del Microsoft.VisualBasic.dll

:ENDOFFILE
popd