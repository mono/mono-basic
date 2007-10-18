echo off


SET VMW4J2EE_DIR=C:\Program Files\Mainsoft\Visual MainWin for J2EE
SET VMW4J2EE_JGAC_DIR=jgac\framework

SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Data.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\vmwutils.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\J2SE.Helpers.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\Microsoft.VisualBasic.jar"

IF NOT DEFINED JAVA_HOME SET JAVA_HOME="C:\jdk1.5.0_06"
echo using JAVA_HOME=%JAVA_HOME%
REM  --- log dir
mkdir logs\mbas

SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v1.1.4322"
echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
SET PATH=%path%;%NET_FRAMEWORK_DIR%

REM echo Convert exe's using GH
REM DIR mbas\Test\tests\*.exe /s /b >mbas.tests.J2EE.to_convert.files.tmp
REM for /f %%f in (mbas.tests.J2EE.to_convert.files.tmp) do CALL build_one_test.J2EE.bat %%f >> %CD%\logs\mbas.tests.J2EE.convert.log 2>&1


echo Run the jar's
DIR mbas\Test\tests\*.jar /s /b >mbas.tests.J2EE.to_run.files.tmp
mkdir logs\mbas
for /f %%f in (mbas.tests.J2EE.to_run.files.tmp) do "%JAVA_HOME%\bin\java.exe" -cp %VMW4J2EE_JGAC_JARS%;%%f;"%CD%\GHTTestLoader.jar" Mainsoft.ght.GHTTestLoader.TestLoader /i:%%~nf /t:1 /n: /m:Main /p: > %CD%\logs\mbas\%%~nf.log 2>&1