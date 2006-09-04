========================
 Microsoft.VisualBasic 
 Visual Studio Projects
========================
.NET 1.1 Visual Studio 2003

 * VS2003 solution and projects - 2003VB.sln
 * 2003VB.vbproj		Microsoft.VisualBasic.dll
 * 2003VB_test_VB.vbproj	NUnit VB tests
 * 2003VB_test.csproj		NUnit C# tests


.NET 2.0 Visual Studio 2005

 * VS2005 solution and projects - 2005VB.sln
 * 2005VB.vbproj		Microsoft.VisualBasic.dll
 * 2005VB_test_VB.vbproj	NUnit VB tests
 * 2005VB_test.csproj		NUnit C# tests

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

convert the Microsoft.VisualBaseic.dll to J2EE java jar
VB.J2EE.build.bat

========================
 run
========================
replace the Microsoft.VisualBaseic.dll at the 1.1 GAC
VB.replace.bat 1

replace the Microsoft.VisualBaseic.dll at the 2.0 GAC
VB.replace.bat 2

restore the Microsoft Microsoft.VisualBaseic.dll at the 1.1 GAC
VB.restore.bat 1

restore the Microsoft Microsoft.VisualBaseic.dll at the 2.0 GAC
VB.restore.bat 2

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

