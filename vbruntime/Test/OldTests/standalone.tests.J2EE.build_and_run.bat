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

mkdir %CD%\logs\standalone
SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v1.1.4322"
echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
SET PATH=%path%;%NET_FRAMEWORK_DIR%

REM Convert The tests
REM DIR standalone\*.exe /s /b >standalone.tests.J2EE.to_convert.files.tmp
REM for /f %%f in (standalone.tests.J2EE.to_convert.files.tmp) do CALL build_one_test.J2EE.bat %%f >> %CD%\logs\standalone.tests.J2EE.convert.log 2>&1


echo run the jar's
DIR standalone\*.jar /s /b >standalone.tests.J2EE.to_run.files.tmp
pushd standalone
for /f %%f in (standalone.tests.J2EE.to_run.files.tmp) do "%JAVA_HOME%\bin\java.exe" -cp %VMW4J2EE_JGAC_JARS%;%%f;"%CD%\GHTTestLoader.jar" Mainsoft.ght.GHTTestLoader.TestLoader /i:%%~nf /t:1 /n: /m:Main /p: > %CD%\logs\standalone\%%~nf.log 2>&1
popd
