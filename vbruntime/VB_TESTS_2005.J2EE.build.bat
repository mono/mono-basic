echo on
echo ====================================
echo = 	Build and Convert the Microsoft.VisualBasic_test_VB.dll into Java jar
echo =	
echo =	NOTE: firest, build the Microsoft.VisualBasic.dll using the TARGET_JVM=True define flag.
echo =	
echo =	sample usage: 
echo =	VB_TESTS_2005.J2EE.build.bat
echo =	
echo ====================================


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

set OUTPUT_FILE_PREFIX=Microsoft.2005_VisualBasic
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log

set VB_COMPILE_OPTIONS_J2EE_VB=/p:DefineConstants="NET_VER=2.0,NET_2_0=True,DEBUG=True,TRACE=True,TARGET_JVM=True"
set VB_COMPILE_OPTIONS_J2EE_CS=/p:DefineConstants="NET_2_0;DEBUG;TRACE;TARGET_JVM;"

echo Building tests solution
msbuild Test\2005VB_test_VB.vbproj %VB_COMPILE_OPTIONS_J2EE_VB% /t:rebuild /p:Configuration=Debug >>%BUILD_LOG% >>2<&1
msbuild Test\2005VB_test_CS.csproj %VB_COMPILE_OPTIONS_J2EE_CS% /t:rebuild /p:Configuration=Debug >>%BUILD_LOG% >>2<&1


IF NOT EXIST "Test\bin\Debug\Microsoft.2005_VisualBasic_test_VB.dll" GOTO EXCEPTION
IF NOT EXIST "Test\bin\Debug\Microsoft.2005_VisualBasic_test.dll" GOTO EXCEPTION

echo == Test dlls exists - start working ... == 

rem set environment settings for running J2EE applications


rem ===========================
rem = SET CLASSPATH and Java options
rem = 
rem = Grasshopper variables and jars
rem ===========================

if not defined VMW_HOME goto VMW_HOME_UNDEFINED
SET VMW4J2EE_DIR=%VMW_HOME%
goto VMW4J2EE_DIR_DEFINED
:VMW_HOME_UNDEFINED
SET VMW4J2EE_DIR=C:\Program Files\Mainsoft for Java EE
:VMW4J2EE_DIR_DEFINED
SET VMW4J2EE_JGAC_DIR=java_refs\framework

IF NOT DEFINED JAVA_HOME SET JAVA_HOME=%VMW4J2EE_DIR%\jre
echo using JAVA_HOME=%JAVA_HOME%

SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Data.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\vmwutils.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\J2SE.Helpers.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\Microsoft.VisualBasic.jar"

SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v2.0.50727"

echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
set path=%path%;%NET_FRAMEWORK_DIR%

set NUNIT_PATH=..\..\nunit20\
set NUNIT_CLASSPATH=%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.framework.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.util.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.core.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit-console.jar

set CLASSPATH=%NUNIT_CLASSPATH%;%VMW4J2EE_JGAC_JARS%



SET TEST_ASSEMBLY=Microsoft.2005_VisualBasic_test_VB

set OUTPUT_FILE_PREFIX=%TEST_ASSEMBLY%
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log
set NUNIT_OPTIONS=/exclude=NotWorking,Broken,TargetJvmNotWorking,TargetJvmNotSupported

echo converting dll to jar without validator
mkdir %CD%\Test\bin\Debug_Java20
"%VMW4J2EE_DIR%\bin\jcsc.exe" %CD%\Test\bin\Debug\%TEST_ASSEMBLY%.dll /debug:3 /novalidator /out:%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar /classpath:%CLASSPATH% /lib:%CD%;"%VMW4J2EE_DIR%\java_refs\jre";"%VMW4J2EE_DIR%\java_refs";C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;enterprise=3D4D0A45DB93955D87296AEC9233A701locale >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
rem echo running java validator
rem "%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW4J2EE_DIR%\bin\validator.jar";"%VMW4J2EE_DIR%\bin\bcel.jar";%CLASSPATH%;"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" >>%BUILD_LOG% 2<&1
rem IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
 
echo Running tests
rem run  Microsoft.VisualBasic_test.jar
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi %NUNIT_OPTIONS% /xml=%TEST_ASSEMBLY%.xml %CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar >>%RUN_LOG% 2<&1

SET TEST_ASSEMBLY=Microsoft.2005_VisualBasic_test

set OUTPUT_FILE_PREFIX=%TEST_ASSEMBLY%
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log

echo converting dll to jar without validator
"%VMW4J2EE_DIR%\bin\jcsc.exe" %CD%\Test\bin\Debug\%TEST_ASSEMBLY%.dll /debug:3 /novalidator /out:%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar /classpath:%CLASSPATH% /lib:%CD%;"%VMW4J2EE_DIR%\java_refs\jre";"%VMW4J2EE_DIR%\java_refs";C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;enterprise=3D4D0A45DB93955D87296AEC9233A701locale >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
rem echo running java validator
rem "%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW4J2EE_DIR%\bin\validator.jar";"%VMW4J2EE_DIR%\bin\bcel.jar";%CLASSPATH%;"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" >>%BUILD_LOG% 2<&1
rem IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
 
echo Running tests
rem run  Microsoft.VisualBasic_test.jar
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi %NUNIT_OPTIONS% /xml=%TEST_ASSEMBLY%.xml %CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar >>%RUN_LOG% 2<&1


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
