echo on
IF NOT DEFINED VSINSTALLDIR echo 'please set VS vars!'
echo running tests using .NET %FRAMEWORKVERSION%

pushd build
DIR ..\..\mcs\class\Microsoft.VisualBasic\Test\standalone\*.vb /s /b >standalone.tests.build.files.tmp
for /f %%f in (standalone.tests.build.files.tmp) do vbc -target:exe -out:%%~pf%%~nf.exe -debug:full -r:mscorlib.dll -r:System.dll -r:Microsoft.VisualBasic.dll %%f ..\..\mcs\class\Microsoft.VisualBasic\Test\standalone\MainModule.vb
popd
