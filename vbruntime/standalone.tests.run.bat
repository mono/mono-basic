echo on
pushd build
DIR ..\..\mcs\class\Microsoft.VisualBasic\Test\standalone\*.exe /s /b >standalone.tests.run.files.tmp
for /f %%f in (standalone.tests.run.files.tmp) do %%~pf%%~nf >%%~pf%%~nf.log 2>&1
popd
