SET BASEDIR=%~dp0..\
SETLOCAL

CALL %BASEDIR%tools\win-make\Make.cmd
IF ERRORLEVEL 1 GOTO ENDFILE
%BASEDIR%tools\win-make\win-make.exe %*
goto ENDFILE

:ENDFILE
ENDLOCAL
