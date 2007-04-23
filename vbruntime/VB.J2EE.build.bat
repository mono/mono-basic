echo off
echo ====================================
echo = 	Build and Convert the Microsoft.VisualBasic.dll into Java jar
echo =	
echo =	NOTE: firest, build the Microsoft.VisualBasic.dll using the TARGET_JVM=True define flag.
echo =	
echo =	sample usage: 
echo =	VB.J2EE.build.bat 2 debug
echo =	
echo ====================================

echo Get batch command parameters.
SET VB_BUILD_PARAM_NET_VERSION=%1
SET VB_BUILD_PARAM_CONFIGURATION=%2

echo Set command parameters default.
IF %VB_BUILD_PARAM_NET_VERSION%=="" SET VB_BUILD_PARAM_NET_VERSION=2
IF %VB_BUILD_PARAM_CONFIGURATION%=="" SET VB_BUILD_PARAM_CONFIGURATION=debug


rem ====================================
rem set environment settings for running J2EE applications
IF NOT DEFINED JAVA_HOME GOTO BADENV
echo using JAVA_HOME=%JAVA_HOME%
IF NOT DEFINED VMW_HOME GOTO BADENV
echo using VMW_HOME=%VMW_HOME%

IF DEFINED VMW4J2EE_JGAC_DIR GOTO JGAC_DEFINED
SET VMW4J2EE_JGAC_DIR=%VMW_HOME%\java_refs\framework
SET VMW4J2EE_JGAC_JRE_DIR=%VMW_HOME%\java_refs\jre
:JGAC_DEFINED
echo using VMW4J2EE_JGAC_DIR=%VMW4J2EE_JGAC_DIR%

echo Build the .NET assembly which will be converted into java.
SET VB_COMPILE_OPTIONS_J2EE=/define:TARGET_JVM=True
SET VB_COMPILE_REFERENCES_J2EE=
call VB.build.bat %VB_BUILD_PARAM_NET_VERSION% %VB_BUILD_PARAM_CONFIGURATION%
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
REM clear environment vars
SET VB_COMPILE_OPTIONS_J2EE=


rem ===========================
rem = SET CLASSPATH and Java options
rem = 
rem = Grasshopper variables and jars
rem ===========================


SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_JGAC_DIR%\System.Data.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_JGAC_DIR%\System.Drawing.jar"


SET BUILD_NET_FRAMEWORK_DIR="%FRAMEWORKDIR%\%FRAMEWORKVERSION%"
echo using BUILD_NET_FRAMEWORK_DIR=%BUILD_NET_FRAMEWORK_DIR%
set path=%path%;%BUILD_NET_FRAMEWORK_DIR%

echo Set log file options.
set startDate=%date%
set startTime=%time%
set sdy=%startDate:~10%
set /a sdm=1%startDate:~4,2% - 100
set /a sdd=1%startDate:~7,2% - 100
set /a sth=%startTime:~0,2%
set /a stm=1%startTime:~3,2% - 100
set /a sts=1%startTime:~6,2% - 100
set TIMESTAMP=%sdy%_%sdm%_%sdd%_%sth%_%stm%

set OUTPUT_FILE_PREFIX=Microsoft_VisualBasic
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log

pushd bin
echo on
echo converting dll to jar without validator
"%VMW_HOME%\bin\jcsc.exe" %CD%\Microsoft.VisualBasic.dll /debug:3 /novalidator /out:%CD%\Microsoft.VisualBasic.jar /classpath:%VMW4J2EE_JGAC_JARS%;%CD%\Microsoft.VisualBasic.jar /lib:%CD%;"%VMW4J2EE_JGAC_JRE_DIR%";"%VMW4J2EE_JGAC_DIR%"; >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
echo running java validator
rem "%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW_HOME%\bin\validator.jar";"%VMW_HOME%\bin\bcel.jar";%VMW4J2EE_JGAC_JARS%;"%CD%\Microsoft.VisualBasic.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\Microsoft.VisualBasic.jar" >>%BUILD_LOG% 2<&1
rem IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
popd

rem copy /Y %CD%\bin\Microsoft.VisualBasic.jar "%VMW4J2EE_JGAC_JARS%\Microsoft.VisualBasic.jar"


:FINALLY
echo ======================
echo finished
echo ======================
GOTO END

:BADENV
echo ======================
echo Error: You must define VMW_HOME and JAVA_HOME 
echo ======================
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
popd
echo ========================
exit /b 1

:END
