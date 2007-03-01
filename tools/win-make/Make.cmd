SET BASEDIR=%~dp0..\..\
SETLOCAL
rem 
rem Find a compiler
rem 
	SET DEFINE=-define:NET_VER=2.0
	SET COMPILERPATH=%WINDIR%\Microsoft.Net\Framework\v2.0.50727\vbc.exe
	IF EXIST "%COMPILERPATH%" (GOTO COMPILERFOUND)
	SET DEFINE=
	SET COMPILERPATH=%WINDIR%\Microsoft.Net\Framework\v1.0.3705\vbc.exe
	IF EXIST "%COMPILERPATH%" (GOTO COMPILERFOUND)
	SET COMPILERPATH=%WINDIR%\Microsoft.Net\Framework\v1.1.4322\vbc.exe
	IF EXIST "%COMPILERPATH%" (GOTO COMPILERFOUND)

	CouldNotFindVBCAnywhere
	echo Could not find vbc.exe anywhere.
	GOTO ENDFILE

:COMPILERFOUND

	%COMPILERPATH% %BASEDIR%tools\win-make\win-make.vb -out:%BASEDIR%tools\win-make\win-make.exe -r:System.dll %DEFINE%

	goto ENDFILE

:ENDFILE
ENDLOCAL
