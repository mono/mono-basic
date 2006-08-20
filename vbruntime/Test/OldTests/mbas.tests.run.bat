echo on
DIR mbas\Test\tests\*.exe /s /b >mbas.tests.run.files.tmp
for /f %%f in (mbas.tests.run.files.tmp) do %%~pf%%~nf.exe >%%~pf%%~nf.log 2>&1

