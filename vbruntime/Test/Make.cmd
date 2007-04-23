

SET PATH1=%WINDIR%\Microsoft.NET\Framework\v1.0.3705
SET PATH1_1=%WINDIR%\Microsoft.NET\Framework\v1.1.4322
SET PATH2=%WINDIR%\Microsoft.NET\Framework\v2.0.50727

SET CSC1=%PATH1%\csc.exe
SET CSC1_1=%PATH1_1%\csc.exe
SET CSC2=%PATH2%\csc.exe

IF EXIST %CSC1% (SET CSC=%CSC1%)
IF EXIST %CSC1_1% (SET CSC=%CSC1_1%)
IF EXIST %CSC2% (SET CSC=%CSC2%)


rem 
rem Build the vbruntime first without signing it, this way it will be linked to the test assembly and we're executing against our vbruntime
rem Then build it signed, so that we can test against the MS vbruntime.
rem 
pushd .
cd ..
SET VB_EXTERNAL_OPTIONS=/define:DONTSIGN=true
CALL VB.build.bat 2 debug
copy bin\Microsoft.VisualBasic.dll Test\Microsoft.VisualBasic.NOTSIGNED.dll
SET VB_EXTERNAL_OPTIONS=
CALL VB.build.bat 2 debug
copy bin\Microsoft.VisualBasic.dll Test\Microsoft.VisualBasic.SIGNED.dll
popd


copy Microsoft.VisualBasic.SIGNED.dll Microsoft.VisualBasic.dll
CALL :EXECUTETEST
copy Microsoft.VisualBasic.NOTSIGNED.dll Microsoft.VisualBasic.dll
CALL :EXECUTETEST
del Microsoft.VisualBasic.*.dll
del Microsoft.VisualBasic.dll

GOTO ENDOFFILE

:EXECUTETEST
pushd .
copy Microsoft.VisualBasic.dll bin
%CSC% "-out:bin\2005VB_test_CS.dll" @2005VB_test_CS.dll.rsp @2005VB_test_CS.dll.sources.win -lib:bin -define:NET_1_0,NET_1_1,NET_2_0
IF ERRORLEVEL 1 (GOTO ENDOFFILE)
cd bin
nunit-console.exe 2005VB_test_CS.dll /labels /noshadow /exclude:Slow
rem del Microsoft.VisualBasic.dll
popd .

:ENDOFFILE