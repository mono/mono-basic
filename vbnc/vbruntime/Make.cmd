
SET FXVERSION=%1

CALL InitPaths.cmd
ECHO ON
IF NOT "%ERRORLEVEL%"=="0" GOTO EOF


pushd .

SET RSPFILE=1
IF "%FXVERSION%"=="2" (SET RSPFILE=2)
IF "%FXVERSION%"=="2_0" (SET RSPFILE=2)

CD Microsoft.VisualBasic
IF "%FXVERSION%"=="1_1" (copy strings1.resources strings.resources /Y)
IF "%FXVERSION%"=="1_0" (copy strings1.resources strings.resources /Y)
IF "%FXVERSION%"=="1" (copy strings1.resources strings.resources /Y)
IF "%FXVERSION%"=="2" (copy strings2.resources strings.resources /Y)
IF "%FXVERSION%"=="2_0" (copy strings2.resources strings.resources /Y)

more strings.txt > strings%RSPFILE%.txt
more strings-only%RSPFILE%.txt >> strings%RSPFILE%.txt
%RESGEN% strings%RSPFILE%.txt

%VBC% @vbruntime%RSPFILE%.rsp @Microsoft.VisualBasic.dll.sources.win %EXTRA_VBRUNTIME_FLAGS%
IF NOT "%ERRORLEVEL%"=="0" SET COMPILATIONERROR="%ERRORLEVEL%"
popd


:EOF