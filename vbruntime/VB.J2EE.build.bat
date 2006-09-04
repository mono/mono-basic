echo off
echo ====================================
echo = 	Build and Convert the Microsoft.VisualBasic.dll into Java jar
echo =	
echo =	NOTE: firest, build the Microsoft.VisualBasic.dll using the TARGET_JVM=True define flag.
echo =	
echo =	sample usage: 
echo =	VB.J2EE.build.bat
echo =	
echo ====================================


SET VB_COMPILE_OPTIONS_J2EE=/define:TARGET_JVM=True
call VB.build.bat 1 debug

rem ====================================
rem set environment settings for running J2EE applications
IF NOT DEFINED JAVA_HOME SET JAVA_HOME="C:\jdk1.5.0_06"
echo using JAVA_HOME=%JAVA_HOME%


rem ===========================
rem = SET CLASSPATH and Java options
rem = 
rem = Grasshopper variables and jars
rem ===========================

SET VMW4J2EE_DIR=C:\Program Files\Mainsoft\Visual MainWin for J2EE
SET VMW4J2EE_JGAC_DIR=jgac\vmw4j2ee_110

SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Data.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\vmwutils.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\J2SE.Helpers.jar"


SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v1.1.4322"
echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
set path=%path%;%NET_FRAMEWORK_DIR%

pushd bin
echo on
echo converting dll to jar
"%VMW4J2EE_DIR%\bin\jcsc.exe" %CD%\Microsoft.VisualBasic.dll /out:%CD%\Microsoft.VisualBasic.jar /classpath:%VMW4J2EE_JGAC_JARS%;%CD%\Microsoft.VisualBasic.jar /lib:%CD%;"%VMW4J2EE_DIR%\jgac\jre5";"%VMW4J2EE_DIR%\jgac"
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
echo running java validator

popd



:FINALLY
echo ======================
echo finished
echo ======================
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
popd
echo ========================
PAUSE

:END
