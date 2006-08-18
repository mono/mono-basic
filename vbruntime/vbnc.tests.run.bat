echo on
pushd build
DIR ..\..\vbnc\vbnc\tests\*.exe /s /b >vbnc.tests.run.files.tmp
for /f "delims=;" %%f in (vbnc.tests.run.files.tmp) do "%%~pf%%~nf" >"%%~pf%%~nf.log" 2>&1
popd
