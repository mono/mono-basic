ECHO OFF
IF NOT "%1"=="" (SET FXVERSION=%1)
IF "%FXVERSION%"=="" (
	ECHO No fx version specified
	ECHO Set FXVERSION to either 1_0, 1_1, 1, 2_0 or 2, or pass the version as a parameter
	SET ERRORLEVEL=1
	GOTO EOF
)


SET FXPATH_1_0=%WINDIR%\Microsoft.NET\Framework\v1.0.3705
SET FXPATH_1_1=%WINDIR%\Microsoft.NET\Framework\v1.1.4322
SET FXPATH_2_0=%WINDIR%\Microsoft.NET\Framework\v2.0.50727

SET CSC_1_0=%FXPATH_1_0%\csc.exe
SET CSC_1_1=%FXPATH_1_1%\csc.exe
SET CSC_2_0=%FXPATH_2_0%\csc.exe

SET VBC_1_0=%FXPATH_1_0%\vbc.exe
SET VBC_1_1=%FXPATH_1_1%\vbc.exe
SET VBC_2_0=%FXPATH_2_0%\vbc.exe

IF NOT EXIST %VBC_1_0% (SET VBC_1_0=%VBC_1_1%)
IF NOT EXIST %CSC_1_0% (SET CSC_1_0=%CSC_1_1%)

IF "%FXVERSION%"=="1_0" (SET VBC=%VBC_1_0%)
IF "%FXVERSION%"=="1_1" (SET VBC=%VBC_1_1%)
IF "%FXVERSION%"=="1" (SET VBC=%VBC_1_1%)
IF "%FXVERSION%"=="2" (SET VBC=%VBC_2_0%)
IF "%FXVERSION%"=="2_0" (SET VBC=%VBC_2_0%)

IF "%FXVERSION%"=="1_0" (SET CSC=%CSC_1_0%)
IF "%FXVERSION%"=="1_1" (SET CSC=%CSC_1_1%)
IF "%FXVERSION%"=="1" (SET CSC=%CSC_1_1%)
IF "%FXVERSION%"=="2" (SET CSC=%CSC_2_0%)
IF "%FXVERSION%"=="2_0" (SET CSC=%CSC_2_0%)

IF "%FXVERSION%"=="1_0" (SET FXPATH=%FXPATH_1_0%)
IF "%FXVERSION%"=="1_1" (SET FXPATH=%FXPATH_1_1%)
IF "%FXVERSION%"=="1" (SET FXPATH=%FXPATH_1_1%)
IF "%FXVERSION%"=="2" (SET FXPATH=%FXPATH_2_0%)
IF "%FXVERSION%"=="2_0" (SET FXPATH=%FXPATH_2_0%)

rem
rem Make a big guess as to where resgen might reside
rem

SET RESGEN2="%PROGRAMFILES%\Microsoft Visual Studio 8\SDK\v2.0\Bin\ResGen.exe"
SET RESGEN1="%PROGRAMFILES%\Microsoft Visual Studio .NET 2003\SDK\v1.1\Bin\ResGen.exe"

IF "%FXVERSION%"=="1_0" (SET RESGEN=%RESGEN1%)
IF "%FXVERSION%"=="1_1" (SET RESGEN=%RESGEN1%)
IF "%FXVERSION%"=="1" (SET RESGEN=%RESGEN1%)
IF "%FXVERSION%"=="2" (SET RESGEN=%RESGEN2%)
IF "%FXVERSION%"=="2_0" (SET RESGEN=%RESGEN2%)

ECHO.
ECHO Paths found (for FXVERSION=%FXVERSION%):
ECHO VBC compiler: %VBC%
ECHO CSC compiler: %CSC%
ECHO.
:EOF