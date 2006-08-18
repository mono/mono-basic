echo on
IF NOT DEFINED VSINSTALLDIR echo 'please set VS vars!'
echo running tests using .NET %FRAMEWORKVERSION%

mkdir build
pushd build
DIR ..\..\vbnc\vbnc\tests\*.vb /s /b >vbnc.tests.build.files.tmp
for /f "delims=;" %%f in (vbnc.tests.build.files.tmp) do vbc -target:exe -out:"%%~pf%%~nf.exe" -debug:full -r:mscorlib.dll -r:System.dll -r:Microsoft.VisualBasic.dll "%%f"

popd
