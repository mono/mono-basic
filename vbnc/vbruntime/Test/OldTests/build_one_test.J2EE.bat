REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM REM 
REM													    REM
REM The scripts mbas.tests.J2EE.convert_and_run.bat AND standalone.tests.J2EE.convert_and_run.bat call 	    REM
REM this one with the full path of the files listed in the tmp file they generate.                          REM
REM The script convert the exe's to jar's with  GH's jcsc.exe and then run GH validation on them  	    REM
REM													    REM
REM													    REM

SET FILE_PATH=%~p1
SET FILE_NAME=%~n1

pushd %FILE_PATH%

"%VMW4J2EE_DIR%\bin\jcsc.exe" "%CD%\%FILE_NAME%.exe" /out:"%CD%\%FILE_NAME%.jar"  /novalidator /classpath:%VMW4J2EE_JGAC_JARS% /lib:%CD%;"%VMW4J2EE_DIR%\jgac\jre5";"%VMW4J2EE_DIR%\jgac"
"%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW4J2EE_DIR%\bin\validator.jar";"%VMW4J2EE_DIR%\bin\bcel.jar";%VMW4J2EE_JGAC_JARS%;"%CD%\%FILE_NAME%.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\%FILE_NAME%.jar" 

popd
