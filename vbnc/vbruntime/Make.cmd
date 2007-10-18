
IF "%FXVERSION%"=="" (SET FXVERSION=%1)

CALL InitPaths.cmd
ECHO ON
IF NOT "%ERRORLEVEL%"=="0" GOTO EOF


pushd .

SET RSPFILE=1
IF "%FXVERSION%"=="2" (SET RSPFILE=2)
IF "%FXVERSION%"=="2_0" (SET RSPFILE=2)

CD Microsoft.VisualBasic
%VBC% @vbruntime%RSPFILE%.rsp @Microsoft.VisualBasic.dll.sources.win %EXTRA_VBRUNTIME_FLAGS%
IF NOT "%ERRORLEVEL%"=="0" SET COMPILATIONERROR="%ERRORLEVEL%"
popd


:EOF