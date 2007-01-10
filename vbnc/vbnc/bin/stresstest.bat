vbnc /codepage:65001 /out:.\vbnc2.exe @..\tests\SelfTest\bootstrap.rsp
if "%ERRORLEVEL%"=="0" echo ************** 1st compilation succeeded **************

vbnc2 /codepage:65001 /out:.\vbnc3.exe @..\tests\SelfTest\bootstrap.rsp
if "%ERRORLEVEL%"=="0" echo ************** 2nd compilation succeeded **************

vbnc3 /codepage:65001 /out:.\vbnc4.exe @..\tests\SelfTest\bootstrap.rsp
if "%ERRORLEVEL%"=="0" echo ************** 3rd compilation succeeded **************

vbnc4 /codepage:65001 /out:.\vbnc5.exe @..\tests\SelfTest\bootstrap.rsp
if "%ERRORLEVEL%"=="0" echo ************** 4th compilation succeeded **************

vbnc5 /codepage:65001 /out:.\vbnc6.exe @..\tests\SelfTest\bootstrap.rsp
if "%ERRORLEVEL%"=="0" echo ************** 5th compilation succeeded **************
