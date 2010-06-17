' FileSystemTest.vb - NUnit Test Cases for Microsoft.VisualBasic.FileIO.FileSystem
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' 
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports FS = Microsoft.VisualBasic.FileIO.FileSystem
Imports System.IO

Namespace FileIO
    <TestFixture()> _
Public Class FileSystemTest
        Private BASEDIR As String
        Private SLOWFILESIZE As Integer = 10000000

        Sub New()
            Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("en-US")
            Threading.Thread.CurrentThread.CurrentCulture = Threading.Thread.CurrentThread.CurrentUICulture
        End Sub

        <SetUp()> _
        Public Sub Init()
            BASEDIR = StripBS(Path.Combine(Environment.CurrentDirectory, "FSTestData")) & Path.DirectorySeparatorChar
            If FS.DirectoryExists(BASEDIR) Then
                FS.DeleteDirectory(BASEDIR, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            FS.CreateDirectory(BASEDIR)
        End Sub

        <TearDown()> _
        Public Sub CleanUp()
            Try
                FS.DeleteDirectory(BASEDIR, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
#If Not TARGET_JVM Then
                Throw ex
#End If
            End Try
        End Sub

        <Test()> _
        Public Sub CombinePathTest()
            Assert.AreEqual(StripBS(IO.Path.Combine(IO.Path.GetFullPath("a"), "b")), FS.CombinePath("a", "b"), "#01")
            Assert.AreEqual(StripBS(IO.Path.Combine(IO.Path.GetFullPath("a/"), "b/")), FS.CombinePath("a/", "b/"), "#02")
            Assert.AreEqual(StripBS(IO.Path.Combine(IO.Path.GetFullPath("a\"), "b\")), FS.CombinePath("a\", "b\"), "#03")
        End Sub

        <Category("Slow")> _
        <Test()> _
        Public Sub CopyDirectoryTest1()
            Dim dira As String = BASEDIR & "CDT1_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")
            Dim filea2 As String = Path.Combine(dira, "filea2")
            Dim filea3 As String = Path.Combine(dira, "filea3")

            Dim dirb As String = BASEDIR & "CDT1_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")
            Dim fileb2 As String = Path.Combine(dirb, "filea2")
            Dim fileb3 As String = Path.Combine(dirb, "filea3")


            Dim smallgarbage As String = "somesmallgarbagehere"
            Dim mediumgarbage As String = CreateBigString(smallgarbage, 1024)
            Dim biggarbage As String = CreateBigString(mediumgarbage, 1024)

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)
            FS.WriteAllText(filea2, mediumgarbage, False)
            FS.WriteAllText(filea3, biggarbage, False)

            FS.CopyDirectory(dira, dirb)

            Helper.CompareDirectory(dira, dirb, "#CDT1-1")

            Try

                FS.CopyDirectory(dira, dirb)
                Assert.Fail("#CDT1-2 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual("Could not complete operation on some files and directories. See the Data property of the exception for more details.", ex.Message, "#CDT1-3")
            Catch ex As Exception
                Assert.Fail("#CDT1-4 Expected 'IOException'")
            End Try
        End Sub

        <Test()> _
        Public Sub CopyDirectoryTest2()
            Dim dira As String = BASEDIR & "CDT2_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")

            Dim dirb As String = BASEDIR & "CDT2_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")


            Dim smallgarbage As String = "thisisgarbage"

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)

            FS.CopyDirectory(dira, dirb)

            Helper.CompareDirectory(dira, dirb, "#CDT2-1")

            Try
                FS.CopyDirectory(dira, dirb)
                Assert.Fail("#CDT2-2 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual("Could not complete operation on some files and directories. See the Data property of the exception for more details.", ex.Message, "#CDT2-3")
            Catch ex As Exception
                Assert.Fail("#CDT2-4 Expected 'IOException'")
            End Try

            Try
                FS.CopyDirectory(dira, dirb, False)
                Assert.Fail("#CDT2-5 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual("Could not complete operation on some files and directories. See the Data property of the exception for more details.", ex.Message, "#CDT2-6")
            Catch ex As Exception
                Assert.Fail("#CDT2-7 Expected 'IOException'")
            End Try
        End Sub

        <Test()> _
        Public Sub CopyDirectoryTest3()
            Dim dira As String = BASEDIR & "CDT3_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")

            Dim dirb As String = BASEDIR & "CDT3_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")


            Dim smallgarbage As String = "moreGarBaGe"

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)

            FS.CopyDirectory(dira, dirb)
            Helper.CompareDirectory(dira, dirb, "#CDT3-1")

            FS.CopyDirectory(dira, dirb, True)
            Helper.CompareDirectory(dira, dirb, "#CDT3-2")

        End Sub

        'TargetJvm - Cancle UI NotSupported
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub CopyDirectoryTest4()
            Dim dira As String = BASEDIR & "CDT4_a"
            Dim dirb As String = BASEDIR & "CDT4_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
            MsgBox("On the next dialog press 'Cancel'")
            Try
                FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
                Assert.Fail("#CDT4-1 Expected 'IOException', got no exception.")
            Catch ex As OperationCanceledException
                Assert.AreEqual("The operation was canceled.", ex.Message, "#CDT4-2")
            Catch ex As Exception
                Assert.Fail("#CDT4-3 Expected 'OperationCanceledException', got '" & ex.GetType.Name & "'")
            End Try
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyDirectoryTest5()
            Dim dira As String = BASEDIR & "CDT5_a"
            Dim dirb As String = BASEDIR & "CDT5_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            MsgBox("On the next dialogs you may press any option")
            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyDirectoryTest6()
            Dim dira As String = BASEDIR & "CDT6_a"
            Dim dirb As String = BASEDIR & "CDT6_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyDirectoryTest7()
            Dim dira As String = BASEDIR & "CDT7_a"
            Dim dirb As String = BASEDIR & "CDT7_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
            FS.CopyDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)

        End Sub

        <Category("Slow")> _
        <Test()> _
        Public Sub CopyFileTest1()
            Dim testname As String = "CFT1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 100000)
            FS.CopyFile(a, b)

            Helper.CompareFile(a, b, testname)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub CopyFileTest2()
            Dim testname As String = "CFT2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            FS.CopyFile(a, b)
            Try
                FS.CopyFile(a, b)
                Assert.Fail(testname & "-1" & " Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual(String.Format("The file '{0}' already exists.", b), ex.Message, testname & "-2")
            End Try

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub CopyFileTest3()
            Dim testname As String = "CFT3"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            FS.CopyFile(a, b)
            FS.CopyFile(a, b, True)
            Helper.CompareFile(a, b, testname)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub CopyFileTest4()
            Dim testname As String = "CFT4"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            FS.CopyFile(a, b)
            Try
                FS.CopyFile(a, b, False)
                Assert.Fail(testname & "-1" & " Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual(String.Format("The file '{0}' already exists.", b), ex.Message, testname & "-2")
            End Try

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyFileTest5()
            Dim testname As String = "CopyFileTest5"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            MsgBox("On the next dialogs you may press any option")
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

            Helper.CompareFile(a, b, testname)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyFileTest6()
            Dim testname As String = "CopyFileTest6"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

            Helper.CompareFile(a, b, testname)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        'TargetJvm - Cancle UI NotSupported
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub CopyFileTest7()
            Dim testname As String = "CopyFileTest7"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
            MsgBox("On the next dialogs press Cancel.")
            Try
                FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
                Assert.Fail(testname & "-1 Expected 'OperationCanceledException'")
            Catch ex As OperationCanceledException
                Assert.AreEqual("The operation was canceled.", ex.Message, testname & "-2")
            End Try

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub CopyFileTest8()
            Dim testname As String = "CopyFileTest8"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
            MsgBox("On the next dialogs press Cancel.")
            FS.CopyFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)


            Helper.CompareFile(a, b, testname)
            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub DeleteDirectoryTest1()
            Dim testname As String = "DeleteDirectoryTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)

            FS.CreateDirectory(dir)
            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.ThrowIfDirectoryNonEmpty)
            Assert.AreEqual(False, FS.DirectoryExists(dir), testname & "-1")

            FS.CreateDirectory(dir)
            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.ThrowIfDirectoryNonEmpty)
            Assert.AreEqual(False, FS.DirectoryExists(dir), testname & "-2")
        End Sub

        <ExpectedException(GetType(IOException))> _
        <Test()> _
        Public Sub DeleteDirectoryTest2()
            Dim testname As String = "DeleteDirectoryTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)

            CreateComplicatedFileHierarchy(dir, False)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.ThrowIfDirectoryNonEmpty)

        End Sub

        <Test()> _
        Public Sub DirectoryExistsTest1()
            Dim testname As String = "DirectoryExistsTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")

            Assert.AreEqual(False, FS.DirectoryExists(dir), testname & "-1")
            FS.CreateDirectory(dir)
            Assert.AreEqual(True, FS.DirectoryExists(dir), testname & "-2")
            CreateFile(file, 1)
            Assert.AreEqual(False, FS.DirectoryExists(file), testname & "-3")
        End Sub

        <Test()> _
        Public Sub DrivesTest1()
            Dim testname As String = "DrivesTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)

            Dim drives As ObjectModel.ReadOnlyCollection(Of DriveInfo)
            Dim realDrives As DriveInfo() = System.IO.DriveInfo.GetDrives()

            drives = FS.Drives

            Assert.AreEqual(realDrives.Length, drives.Count, testname & "-1")
        End Sub

        <Test()> _
        Public Sub FileExistsTest1()
            Dim testname As String = "FileExistsTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")

            Assert.AreEqual(False, FS.FileExists(file), testname & "-0")
            Assert.AreEqual(False, FS.FileExists(dir), testname & "-1")
            FS.CreateDirectory(dir)
            Assert.AreEqual(False, FS.FileExists(dir), testname & "-2")
            Assert.AreEqual(False, FS.FileExists(file), testname & "-3")
            CreateFile(file, 1)
            Assert.AreEqual(True, FS.FileExists(file), testname & "-3")
        End Sub

        <Test()> _
        Public Sub FindInFilesTest1()
            Dim testname As String = "FindInFilesTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(14, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub

        <Test()> _
        Public Sub FindInFilesTest2()
            Dim testname As String = "FindInFilesTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String() = New String() {"??"}

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub

        <Test()> _
        Public Sub FindInFilesTest3()
            Dim testname As String = "FindInFilesTest3"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String() = New String() {"a*", "?b", "??"}

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(14, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub


        <Test()> _
        Public Sub FindInFilesTest4()
            Dim testname As String = "FindInFilesTest4"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String() = New String() {"?"}

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub


        <Test()> _
      Public Sub FindInFilesTest5()
            Dim testname As String = "FindInFilesTest5"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String() = New String() {"a?"}

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub

        <Test()> _
      Public Sub FindInFilesTest6()
            Dim testname As String = "FindInFilesTest6"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String() = New String() {"[abc]?"}

            CreateComplicatedFileHierarchy(dir, True, System.Text.Encoding.ASCII.GetBytes("FINDME"))

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-1")

            found = FS.FindInFiles(dir, "D", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-2")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-3")

            found = FS.FindInFiles(dir, "D", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-4")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            found = FS.FindInFiles(dir, "d", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-6")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-7")

            found = FS.FindInFiles(dir, "d", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-8")


            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-11")

            found = FS.FindInFiles(dir, "FINDME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-12")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-13")

            found = FS.FindInFiles(dir, "FINDME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-14")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-15")

            found = FS.FindInFiles(dir, "findme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-16")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-17")

            found = FS.FindInFiles(dir, "findme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-18")



            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-21")

            found = FS.FindInFiles(dir, "NOTME", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-22")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-23")

            found = FS.FindInFiles(dir, "NOTME", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-24")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            found = FS.FindInFiles(dir, "notme", False, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-26")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-27")

            found = FS.FindInFiles(dir, "notme", True, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-28")

        End Sub


        <Test()> _
        Public Sub GetDirectories1()
            Dim testname As String = "GetDirectories1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String()

            CreateComplicatedFileHierarchy(dir, False)

            found = FS.GetDirectories(dir)
            Assert.AreEqual(2, found.Count, testname & "-1")


            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(10, found.Count, testname & "-2")
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(2, found.Count, testname & "-3")


            patterns = New String() {"[abc]?"}
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-4")
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            patterns = New String() {"*"}
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(10, found.Count, testname & "-14")
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(2, found.Count, testname & "-15")

            patterns = New String() {"a*"}
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-24")
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(1, found.Count, testname & "-25")

            patterns = New String() {"a*", "*b"}
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(2, found.Count, testname & "-34")
            found = FS.GetDirectories(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(1, found.Count, testname & "-35")

        End Sub

        <Test()> _
        Public Sub GetDirectoryInfoTest1()
            Dim testname As String = "GetDirectoryInfoTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)

            FS.CreateDirectory(dir)

            Assert.AreEqual(FS.GetDirectoryInfo(dir).ToString, New DirectoryInfo(dir).ToString, testname & "-1")

        End Sub

        <Test()> _
        Public Sub GetDriveInfoTest1()
            Dim testname As String = "GetDriveInfoTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)

            FS.CreateDirectory(dir)
            Dim dir1 As DriveInfo = FS.GetDriveInfo(dir)
            Dim dir2 As DriveInfo = New DriveInfo(dir)
            Assert.IsTrue(dir1.RootDirectory().Exists)
            Assert.AreEqual(dir1.ToString, dir2.ToString, testname & "-1")

        End Sub

        <Test()> _
        Public Sub GetFileInfoTest1()
            Dim testname As String = "GetFileInfoTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")

            FS.CreateDirectory(dir)
            CreateFile(file, 1)

            Assert.AreEqual(FS.GetFileInfo(file).ToString, New FileInfo(file).ToString, testname & "-1")

        End Sub

        <Test()> _
        Public Sub GetFiles1()
            Dim testname As String = "GetFiles1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")
            Dim found As ObjectModel.ReadOnlyCollection(Of String)
            Dim patterns As String()


            CreateComplicatedFileHierarchy(dir, False)

            found = FS.GetFiles(dir)
            Assert.AreEqual(1, found.Count, testname & "-1")


            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
            Assert.AreEqual(13, found.Count, testname & "-2")
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly)
            Assert.AreEqual(1, found.Count, testname & "-3")


            patterns = New String() {"[abc]?"}
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(0, found.Count, testname & "-4")
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-5")

            patterns = New String() {"*"}
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(13, found.Count, testname & "-14")
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(1, found.Count, testname & "-15")

            patterns = New String() {"a*"}
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(1, found.Count, testname & "-24")
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-25")

            patterns = New String() {"a*", "*b"}
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, patterns)
            Assert.AreEqual(2, found.Count, testname & "-34")
            found = FS.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, patterns)
            Assert.AreEqual(0, found.Count, testname & "-35")

        End Sub

        <Test()> _
        Public Sub GetNameTest1()
            Dim testname As String = "GetNameTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")

            Assert.AreEqual("a", FS.GetName("a"), testname & "-1")
            Assert.AreEqual(IO.Path.GetFileName(file), FS.GetName(file), testname & "-2")
            Assert.AreEqual(IO.Path.GetFileName(dir), FS.GetName(dir), testname & "-3")
        End Sub

        <Test()> _
        Public Sub GetParentPathTest1()
            Dim testname As String = "GetParentPathTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim file As String = Path.Combine(dir, "file")

            Assert.AreEqual("", FS.GetParentPath("a"), testname & "-1")
            Assert.AreEqual(IO.Path.GetDirectoryName(file), FS.GetParentPath(file), testname & "-2")
            Assert.AreEqual(IO.Path.GetDirectoryName(dir), FS.GetParentPath(dir), testname & "-3")
        End Sub

        <Test()> _
        Public Sub GetTempFileNameTest1()
            Dim testname As String = "GetTempFileNameTest1"

            Dim file, file2 As String

            file = FS.GetTempFileName()
            file2 = System.IO.Path.GetTempFileName

            Assert.AreEqual(System.IO.Path.GetDirectoryName(file), System.IO.Path.GetDirectoryName(file2), testname & "-1")

            IO.File.Delete(file)
            IO.File.Delete(file2)
        End Sub

        <Category("Slow")> _
        <Test()> _
        Public Sub MoveDirectoryTest1()
            Dim dira As String = BASEDIR & "MDT1_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")
            Dim filea2 As String = Path.Combine(dira, "filea2")
            Dim filea3 As String = Path.Combine(dira, "filea3")

            Dim dirb As String = BASEDIR & "MDT1_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")
            Dim fileb2 As String = Path.Combine(dirb, "filea2")
            Dim fileb3 As String = Path.Combine(dirb, "filea3")


            Dim smallgarbage As String = "somesmallgarbagehere"
            Dim mediumgarbage As String = CreateBigString(smallgarbage, 1024)
            Dim biggarbage As String = CreateBigString(mediumgarbage, 1024)

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)
            FS.WriteAllText(filea2, mediumgarbage, False)
            FS.WriteAllText(filea3, biggarbage, False)

            FS.MoveDirectory(dira, dirb)

            Try
                FS.MoveDirectory(dira, dirb)
                Assert.Fail("#MDT1-2 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual(String.Format("Could not find directory '{0}'.", dira), ex.Message, "#MDT1-3")
            Catch ex As Exception
                Assert.Fail("#MDT1-4 Expected 'IOException'")
            End Try
        End Sub

        <Test()> _
        Public Sub MoveDirectoryTest2()
            Dim dira As String = BASEDIR & "MDT2_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")

            Dim dirb As String = BASEDIR & "MDT2_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")

            Dim smallgarbage As String = "thisisgarbage"

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)

            FS.MoveDirectory(dira, dirb)


            Try
                FS.MoveDirectory(dira, dirb)
                Assert.Fail("#MDT2-2 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual(String.Format("Could not find directory '{0}'.", dira), ex.Message, "#MDT2-3")
            Catch ex As Exception
                Assert.Fail("#MDT2-4 Expected 'IOException'")
            End Try

            Try
                FS.MoveDirectory(dira, dirb, False)
                Assert.Fail("#MDT2-5 Expected 'IOException'")
            Catch ex As IOException
                Assert.AreEqual(String.Format("Could not find directory '{0}'.", dira), ex.Message, "#MDT2-6")
            Catch ex As Exception
                Assert.Fail("#MDT2-7 Expected 'IOException'")
            End Try
        End Sub

        <Test()> _
        Public Sub MoveDirectoryTest3()
            Dim dira As String = BASEDIR & "MDT3_a"
            Dim filea1 As String = Path.Combine(dira, "filea1")

            Dim dirb As String = BASEDIR & "MDT3_b"
            Dim fileb1 As String = Path.Combine(dirb, "filea1")


            Dim smallgarbage As String = "moreGarBaGe"

            FS.CreateDirectory(dira)
            FS.WriteAllText(filea1, smallgarbage, False)

            FS.MoveDirectory(dira, dirb)


        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub MoveDirectoryTest5()
            Dim dira As String = BASEDIR & "MDT5_a"
            Dim dirb As String = BASEDIR & "MDT5_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            'MsgBox("On the next dialogs you may press any option")
            'FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub MoveDirectoryTest6()
            Dim dira As String = BASEDIR & "MDT6_a"
            Dim dirb As String = BASEDIR & "MDT6_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            'FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub MoveDirectoryTest7()
            Dim dira As String = BASEDIR & "MDT7_a"
            Dim dirb As String = BASEDIR & "MDT7_b"

            CreateComplicatedFileHierarchy(dira, True)

            FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
            'FS.MoveDirectory(dira, dirb, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)

        End Sub

        <Category("Slow")> _
              <Test()> _
              Public Sub MoveFileTest1()
            Dim testname As String = "MFT1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 100000)
            FS.MoveFile(a, b)
            CreateFile(a, 100000)

            Helper.CompareFile(a, b, testname)

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub MoveFileTest2()
            Dim testname As String = "MFT2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            FS.MoveFile(a, b)
            Try
                FS.MoveFile(a, b)
                Assert.Fail(testname & "-1" & " Expected 'IOException'")
            Catch ex As IOException
#If Not TARGET_JVM Then
                Assert.AreEqual(String.Format("Could not find file '{0}'.", a), ex.Message, testname & "-2")
#End If
            End Try

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub MoveFileTest3()
            Dim testname As String = "MFT3"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            CreateFile(b, 10)
            FS.MoveFile(a, b, True)
            Assert.AreEqual(True, FS.FileExists(b), testname & "-1")
            Assert.AreEqual(False, FS.FileExists(a), testname & "-2")

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub MoveFileTest4()
            Dim testname As String = "MFT4"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, 10)
            CreateFile(b, 10)
            Try
                FS.MoveFile(a, b, False)
                Assert.Fail(testname & "-1" & " Expected 'IOException'")
            Catch ex As IOException
                'Assert.AreEqual(String.Format("It is not possible to create a file that already exists." + Environment.NewLine, b), ex.Message, testname & "-2")
            End Try

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        'TargetJvm - Option Dialogs NotSupported
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub MoveFileTest5()
            Dim testname As String = "MoveFileTest5"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            CreateFile(b, SLOWFILESIZE)
            MsgBox("On the next dialogs press 'Yes'")
            FS.MoveFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

            Assert.AreEqual(True, FS.FileExists(b), testname & "-1")
            Assert.AreEqual(False, FS.FileExists(a), testname & "-2")

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub MoveFileTest6()
            Dim testname As String = "MoveFileTest6"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            CreateFile(b, SLOWFILESIZE)
            FS.MoveFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

            Assert.AreEqual(True, FS.FileExists(b), testname & "-1")
            Assert.AreEqual(False, FS.FileExists(a), testname & "-2")

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub


        <Category("UI")> _
        <Test()> _
        Public Sub MoveFileTest7()
            Dim testname As String = "MoveFileTest7"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            CreateFile(b, SLOWFILESIZE)
            MsgBox("On the next dialogs press 'No'.")
            Try
                FS.MoveFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)
                Assert.Fail(testname & "-1 Expected 'OperationCanceledException'")
            Catch ex As OperationCanceledException
                Assert.AreEqual("The operation was canceled.", ex.Message, testname & "-2")
            End Try

            Assert.AreEqual(True, FS.FileExists(b), testname & "-3")
            Assert.AreEqual(True, FS.FileExists(a), testname & "-4")

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Category("UI")> _
        <Test()> _
        Public Sub MoveFileTest8()
            Dim testname As String = "MoveFileTest8"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")
            Dim b As String = Path.Combine(dir, "b.txt")

            FS.CreateDirectory(dir)

            CreateFile(a, SLOWFILESIZE)
            CreateFile(b, SLOWFILESIZE)

            FS.MoveFile(a, b, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException)

            Assert.AreEqual(True, FS.FileExists(b), testname & "-1")
            Assert.AreEqual(False, FS.FileExists(a), testname & "-2")

            FS.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        <Test()> _
        Public Sub OpenTextFieldParserTest1()
            Dim testname As String = "OpenTextFieldParserTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.WriteAllText(a, "LINE", False)

            Using parser As Microsoft.VisualBasic.FileIO.TextFieldParser = FS.OpenTextFieldParser(a)
                Assert.AreEqual(parser.ReadLine, "LINE", testname & "-1")
            End Using
        End Sub

        <Test()> _
        Public Sub OpenTextFieldParserTest2()
            Dim testname As String = "OpenTextFieldParserTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.WriteAllText(a, "LINE" & vbNewLine & "a;b", False)

            Using parser As Microsoft.VisualBasic.FileIO.TextFieldParser = FS.OpenTextFieldParser(a, ";")
                Assert.AreEqual("LINE", parser.ReadLine, testname & "-1")
                Assert.AreEqual("a?b", Join(parser.ReadFields(), "?"), testname & "-2")
            End Using
        End Sub

        <Test()> _
        Public Sub OpenTextFieldParserTest3()
            Dim testname As String = "OpenTextFieldParserTest3"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.WriteAllText(a, "LINE" & vbNewLine & "abc", False)

            Using parser As Microsoft.VisualBasic.FileIO.TextFieldParser = FS.OpenTextFieldParser(a, 1, 2)
                Assert.AreEqual("LINE", parser.ReadLine, testname & "-1")
                Assert.AreEqual("a?bc", Join(parser.ReadFields(), "?"), testname & "-2")
            End Using
        End Sub


        <Test()> _
        Public Sub OpenTextFileReaderTest1()
            Dim testname As String = "OpenTextFileReaderTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.WriteAllText(a, "LINE" & vbNewLine & "a;b", False)

            Using parser As StreamReader = FS.OpenTextFileReader(a)
                Assert.AreEqual("LINE", parser.ReadLine, testname & "-1")
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, parser.CurrentEncoding.EncodingName, testname & "-2")
            End Using
        End Sub

        <Test()> _
               Public Sub OpenTextFileReaderTest2()
            Dim testname As String = "OpenTextFileReaderTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.WriteAllText(a, "LINE", False)

            Using parser As StreamReader = FS.OpenTextFileReader(a, System.Text.Encoding.Unicode)
                Assert.AreEqual("LINE", parser.ReadLine, testname & "-1")
                Assert.AreEqual(True, parser.BaseStream.Length = parser.BaseStream.Position, testname & "-2")
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, parser.CurrentEncoding.EncodingName, testname & "-3")
            End Using
        End Sub


        <Test()> _
        Public Sub OpenTextFileWriterTest1()
            Dim testname As String = "OpenTextFileWriterTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            Using writer As StreamWriter = FS.OpenTextFileWriter(a, False)
                writer.Write(contents)
            End Using

            Using reader As StreamReader = FS.OpenTextFileReader(a)
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-1")
                Assert.AreEqual(contents, reader.ReadToEnd, testname & "-2")
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-3")
            End Using

            Using writer As StreamWriter = FS.OpenTextFileWriter(a, True)
                writer.Write(contents)
            End Using
            contents &= contents
            Using reader As StreamReader = FS.OpenTextFileReader(a)
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-4")
                Assert.AreEqual(contents, reader.ReadToEnd, testname & "-5")
                Assert.AreEqual(System.Text.Encoding.UTF8.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-6")
            End Using


        End Sub

        <Test()> _
        Public Sub OpenTextFileWriterTest2()
            Dim testname As String = "OpenTextFileWriterTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine
            Using writer As StreamWriter = FS.OpenTextFileWriter(a, False, System.Text.Encoding.ASCII)
                writer.Write(contents)
            End Using

            Using reader As StreamReader = FS.OpenTextFileReader(a, System.Text.Encoding.ASCII)
                Assert.AreEqual(System.Text.Encoding.ASCII.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-1")
                Assert.AreEqual(contents, reader.ReadToEnd, testname & "-2")
                Assert.AreEqual(System.Text.Encoding.ASCII.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-3")
            End Using

            Using writer As StreamWriter = FS.OpenTextFileWriter(a, True, System.Text.Encoding.ASCII)
                writer.Write(contents)
            End Using
            wrongContents &= wrongContents
            Using reader As StreamReader = FS.OpenTextFileReader(a, System.Text.Encoding.ASCII)
                Assert.AreEqual(System.Text.Encoding.ASCII.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-4")
                Assert.AreEqual(contents & contents, reader.ReadToEnd, testname & "-5")
                Assert.AreEqual(System.Text.Encoding.ASCII.EncodingName, reader.CurrentEncoding.EncodingName, testname & "-6")
            End Using
        End Sub

        <Test()> _
             Public Sub ReadAllBytesTest1()
            Dim testname As String = "ReadAllBytesTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine
            Using writer As StreamWriter = FS.OpenTextFileWriter(a, False, System.Text.Encoding.ASCII)
                writer.Write(contents)
            End Using

            Dim bytes() As Byte
            bytes = FS.ReadAllBytes(a)

            Assert.AreEqual(contents, System.Text.Encoding.ASCII.GetString(bytes), testname & "-1")
        End Sub

        <Test()> _
        Public Sub ReadAllTextTest1()
            Dim testname As String = "ReadAllTextTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine
            Using writer As StreamWriter = FS.OpenTextFileWriter(a, False, System.Text.Encoding.ASCII)
                writer.Write(contents)
            End Using

            Dim result As String
            result = FS.ReadAllText(a)

            Assert.AreEqual(contents, contents, testname & "-1")
        End Sub

        <Test()> _
        Public Sub RenameDirectoryTest1()
            Dim testname As String = "RenameDirectoryTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim dirb As String = Path.Combine(BASEDIR, testname & "-new")
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            FS.RenameDirectory(dir, testname & "-new")
            Assert.AreEqual(False, FS.DirectoryExists(dir), testname & "-1")
            Assert.AreEqual(True, FS.DirectoryExists(dirb), testname & "-2")
        End Sub

        <Test()> _
Public Sub RenameFileTest1()
            Dim testname As String = "RenameFileTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim b As String = Path.Combine(dir, "b.txt")
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)
            CreateFile(a, 1)

            FS.RenameFile(a, "b.txt")
            Assert.AreEqual(False, FS.FileExists(a), testname & "-1")
            Assert.AreEqual(True, FS.FileExists(b), testname & "-2")
        End Sub


        <Test()> _
        Public Sub ReadAllTextTest2()
            Dim testname As String = "ReadAllTextTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine
            Using writer As StreamWriter = FS.OpenTextFileWriter(a, False, System.Text.Encoding.ASCII)
                writer.Write(contents)
            End Using

            Dim result As String

            result = FS.ReadAllText(a, System.Text.Encoding.ASCII)
            Assert.AreEqual(contents, contents, testname & "-1")


            result = FS.ReadAllText(a, System.Text.Encoding.UTF8)
            Assert.AreEqual(contents, contents, testname & "-2")
        End Sub


        <Test()> _
             Public Sub WriteAllBytesTest1()
            Dim testname As String = "WriteAllBytesTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)


            Dim contents As String
            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine

            Dim bytes(), bytesb() As Byte

            bytes = System.Text.Encoding.BigEndianUnicode.GetBytes(contents)
            FS.WriteAllBytes(a, bytes, False)
            bytesb = FS.ReadAllBytes(a)
            Helper.CompareBytes(bytes, bytesb, testname & "-1")

            FS.WriteAllBytes(a, bytes, True)
            bytesb = FS.ReadAllBytes(a)
            bytes = System.Text.Encoding.BigEndianUnicode.GetBytes(contents & contents)
            Helper.CompareBytes(bytes, bytesb, testname & "-2")
        End Sub

        <Test()> _
        Public Sub WriteAllTextTest1()
            Dim testname As String = "WriteAllTextTest1"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String
            Dim result As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine

            FS.WriteAllText(a, contents, False)
            result = FS.ReadAllText(a)
            Assert.AreEqual(contents, result, testname & "-1")

            FS.WriteAllText(a, contents, True)
            result = FS.ReadAllText(a)
            Assert.AreEqual(contents & contents, result, testname & "-2")
        End Sub

        <Test()> _
        Public Sub WriteAllTextTest2()
            Dim testname As String = "WriteAllTextTest2"
            Dim dir As String = Path.Combine(BASEDIR, testname)
            Dim a As String = Path.Combine(dir, "a.txt")

            FS.CreateDirectory(dir)

            Dim contents As String
            Dim wrongContents As String
            Dim result As String

            contents = "Line1" & vbNewLine & "Line2" & vbNewLine & "`-192" & vbNewLine
            wrongContents = "Line1" & vbNewLine & "Line2" & vbNewLine & "??`-?192" & vbNewLine

            FS.WriteAllText(a, contents, False, System.Text.Encoding.BigEndianUnicode)
            result = FS.ReadAllText(a)
            Assert.AreEqual(contents, result, testname & "-1")

            FS.WriteAllText(a, contents, True, System.Text.Encoding.BigEndianUnicode)
            result = FS.ReadAllText(a)
            Assert.AreEqual(contents & contents, result, testname & "-2")
        End Sub

#Region "Helper functions"
        Private Sub MsgBox(ByVal Message As String, Optional ByVal Style As Microsoft.VisualBasic.MsgBoxStyle = Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Information)
#If Not TARGET_JVM Then
            Microsoft.VisualBasic.Interaction.MsgBox(Message, Style Or Microsoft.VisualBasic.MsgBoxStyle.SystemModal)
#End If
        End Sub

        Private Sub CreateFile(ByVal Name As String, ByVal Size As Integer)
            Dim buffer(1023) As Byte
            Dim written As Integer

            Using writer As New IO.FileStream(Name, FileMode.Create, FileAccess.Write, FileShare.Read)
                Do Until written >= Size
                    Dim write As Integer
                    write = Math.Min(1024, Size - written)
                    If write = 0 Then Exit Do
                    writer.Write(buffer, 0, write)
                    written += write
                Loop
            End Using
            'Console.WriteLine("Written " & written & " bytes to " & Name)
        End Sub

        Private Sub CreateComplicatedFileHierarchy(ByVal BasePath As String, ByVal anyBigFiles As Boolean, Optional ByVal contents As Byte() = Nothing)
            'Create some directories
            Dim a, b, c, d, e, f, g, h, i, j As String
            a = Path.Combine(BasePath, "a")
            b = Path.Combine(a, "b")
            c = Path.Combine(b, "c")
            d = Path.Combine(b, "d")
            e = Path.Combine(a, "e")
            f = Path.Combine(b, "f")
            g = Path.Combine(e, "g")
            h = Path.Combine(d, "h")
            i = Path.Combine(g, "i")
            j = Path.Combine(BasePath, "j")

            FS.CreateDirectory(a)
            FS.CreateDirectory(b)
            FS.CreateDirectory(c)
            FS.CreateDirectory(d)
            FS.CreateDirectory(e)
            FS.CreateDirectory(f)
            FS.CreateDirectory(g)
            FS.CreateDirectory(h)
            FS.CreateDirectory(i)
            FS.CreateDirectory(j)

            'Create some small files
            Dim aa, bb, cc, dd, ee, ff, gg, hh, ii, jj, kk, ll, mm As String
            aa = Path.Combine(a, "aa")
            bb = Path.Combine(a, "bb")
            cc = Path.Combine(a, "cc")
            dd = Path.Combine(c, "dd")
            ee = Path.Combine(e, "ee")
            ff = Path.Combine(e, "ff")
            gg = Path.Combine(e, "gg")
            hh = Path.Combine(i, "hh")
            ii = Path.Combine(f, "ii")
            jj = Path.Combine(f, "jj")
            kk = Path.Combine(f, "kk")
            ll = Path.Combine(i, "ll")
            mm = Path.Combine(BasePath, "mm")

            If contents Is Nothing Then
                FS.WriteAllBytes(aa, System.Text.Encoding.UTF32.GetBytes(aa), False)
                FS.WriteAllBytes(bb, System.Text.Encoding.UTF32.GetBytes(bb), False)
                FS.WriteAllBytes(cc, System.Text.Encoding.UTF32.GetBytes(cc), False)
                FS.WriteAllBytes(dd, System.Text.Encoding.UTF32.GetBytes(dd), False)
                FS.WriteAllBytes(ee, System.Text.Encoding.UTF32.GetBytes(ee), False)
                FS.WriteAllBytes(ff, System.Text.Encoding.UTF32.GetBytes(ff), False)
                FS.WriteAllBytes(gg, System.Text.Encoding.UTF32.GetBytes(gg), False)
                FS.WriteAllBytes(hh, System.Text.Encoding.UTF32.GetBytes(hh), False)
                FS.WriteAllBytes(ii, System.Text.Encoding.UTF32.GetBytes(ii), False)
                FS.WriteAllBytes(jj, System.Text.Encoding.UTF32.GetBytes(jj), False)
                FS.WriteAllBytes(kk, System.Text.Encoding.UTF32.GetBytes(kk), False)
                FS.WriteAllBytes(ll, System.Text.Encoding.UTF32.GetBytes(ll), False)
                FS.WriteAllBytes(mm, System.Text.Encoding.UTF32.GetBytes(mm), False)
                If anyBigFiles Then
                    Dim xx, yy, zz As String
                    zz = Path.Combine(BasePath, "zz")
                    yy = Path.Combine(a, "yy")
                    xx = Path.Combine(d, "xx")

                    FS.WriteAllText(zz, CreateBigString(zz, 1024), False)
                    FS.WriteAllText(zz, CreateBigString(zz, 1024 * 64), False)
                    FS.WriteAllText(zz, CreateBigString(zz, 1024 * 256), False)
                End If
            Else
                FS.WriteAllBytes(aa, contents, False)
                FS.WriteAllBytes(bb, contents, False)
                FS.WriteAllBytes(cc, contents, False)
                FS.WriteAllBytes(dd, contents, False)
                FS.WriteAllBytes(ee, contents, False)
                FS.WriteAllBytes(ff, contents, False)
                FS.WriteAllBytes(gg, contents, False)
                FS.WriteAllBytes(hh, contents, False)
                FS.WriteAllBytes(ii, contents, False)
                FS.WriteAllBytes(jj, contents, False)
                FS.WriteAllBytes(kk, contents, False)
                FS.WriteAllBytes(ll, contents, False)
                FS.WriteAllBytes(mm, contents, False)
                If anyBigFiles Then
                    Dim xx, yy, zz As String
                    zz = Path.Combine(BasePath, "zz")
                    yy = Path.Combine(a, "yy")
                    xx = Path.Combine(d, "xx")

                    FS.WriteAllText(zz, CreateBigString(System.Text.Encoding.ASCII.GetString(contents), 1024), False)
                    FS.WriteAllText(zz, CreateBigString(System.Text.Encoding.ASCII.GetString(contents), 1024 * 64), False)
                    FS.WriteAllText(zz, CreateBigString(System.Text.Encoding.ASCII.GetString(contents), 1024 * 256), False)
                End If
            End If

        End Sub


        Private Shared Function AsString(ByVal i As IDictionary) As String
            Dim result As String = ""

            For Each key As Object In i.Keys
                result &= key.ToString() & "=" & i.Item(key).ToString & vbNewLine
            Next

            Return result
        End Function

        Private Function CreateBigString(ByVal base As String, ByVal repeats As Integer) As String
            Dim builder As New System.Text.StringBuilder

            builder.Capacity = base.Length * repeats + 1

            For i As Integer = 1 To repeats
                builder.Append(base)
            Next

            Return builder.ToString
        End Function


        Private Function StripBS(ByVal path As String) As String
            Return path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
        End Function
#End Region
    End Class
End Namespace