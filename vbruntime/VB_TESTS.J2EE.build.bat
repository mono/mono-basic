echo on
echo ====================================
echo = 	Build and Convert the Microsoft.VisualBasic_test_VB.dll into Java jar
echo =	
echo =	NOTE: firest, build the Microsoft.VisualBasic.dll using the TARGET_JVM=True define flag.
echo =	
echo =	sample usage: 
echo =	VB_TESTS.J2EE.build.bat
echo =	
echo ====================================



SET VB_COMPILE_OPTIONS_J2EE=/define:TARGET_JVM=True

IF NOT EXIST "bin\Microsoft.VisualBasic_test_VB.dll" GOTO EXCEPTION

echo == Microsoft.VisualBasic_test_VB.dll exist, start working on it == 

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
SET VMW4J2EE_JGAC_DIR=jgac\framework

SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Data.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\vmwutils.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\J2SE.Helpers.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\Microsoft.VisualBasic.jar"



SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v1.1.4322"
echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
set path=%path%;%NET_FRAMEWORK_DIR%

set NUNIT_PATH=..\..\mcs\nunit20\
set NUNIT_CLASSPATH=%NUNIT_PATH%nunit-console\bin\Debug_Java\nunit.framework.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java\nunit.util.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java\nunit.core.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java\nunit-console.jar

 "%VS71COMNTOOLS%..\IDE\devenv.com" %CD%\2003VB.J2EE.sln /rebuild Debug_Java
 IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
set CLASSPATH=%NUNIT_CLASSPATH%;%VMW4J2EE_JGAC_JARS%
rem run  Microsoft.VisualBasic_test.jar
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi /xml=Microsoft.VisualBasic_test.xml %CD%\bin_Java\Microsoft.VisualBasic_test.jar 
rem  run Microsoft.VisualBasic_test_VB.jar 
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi /xml=Microsoft.VisualBasic_test_VB.xml %CD%\bin_Java\Microsoft.VisualBasic_test_VB.jar 
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION

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
