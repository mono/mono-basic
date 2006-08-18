========================
 Microsoft.VisualBasic 
 Visual Studio Projects
========================
VS2003 solution and project - 2003VB.sln, 2003VB.vbproj
VS2005 solution and project - 2005VB.sln, 2005VB.vbproj


========================
 build
========================
build Microsoft.VisualBaseic.dll using .NET 1.1 on debug.
VB.build.bat 1 debug

build Microsoft.VisualBaseic.dll using .NET 1.1 on release.
VB.build.bat 1 release


build Microsoft.VisualBaseic.dll using .NET 2.0 on debug.
VB.build.bat 2 debug

build Microsoft.VisualBaseic.dll using .NET 2.0 on release.
VB.build.bat 2 release

========================
 run
========================
replace the Microsoft.VisualBaseic.dll at the GAC
VB.replace.bat

restore the Microsoft.VisualBaseic.dll at the GAC
VB.restore.bat

========================
 tests
========================
Note: before running the tests, replace the Microsoft.VisualBaseic.dll at the GAC

mbas.tests.build.bat
mbas.tests.run.bat

standalone.tests.build.bat
standalone.tests.run.bat

vbnc.tests.build.bat
vbnc.tests.run.bat

