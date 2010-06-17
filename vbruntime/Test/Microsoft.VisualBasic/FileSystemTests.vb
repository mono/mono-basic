'
' FileSystemTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Information 
'
' Guy Cohen (guyc@mainsoft.com)
'
' 
'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
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
'

Imports NUnit.Framework
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Threading
Imports Microsoft.VisualBasic

<Category("Broken"), TestFixture(), Category("MayFailOnSharedDrived")> _
Public Class FilesSystemTest
    Public DATA_DIR As String
    Public sep_ch As Char


    <TestFixtureSetUp()> _
    Public Sub GetReady()
        DATA_DIR = Environment.GetEnvironmentVariable("DATA_DIR")
        sep_ch = Path.DirectorySeparatorChar
        If Not (DATA_DIR) Then
            'System.Console.WriteLine("DATA_DIR environment variable not found, set default value")
            DATA_DIR = (Directory.GetCurrentDirectory() + sep_ch + "data")
        End If
        If Not Directory.Exists(DATA_DIR) Then
            Directory.CreateDirectory(DATA_DIR)
        Else
            Directory.Delete(DATA_DIR, True)
            Directory.CreateDirectory(DATA_DIR)
        End If
    End Sub

    <TestFixtureTearDown()> _
    Public Sub Clean_All()


    End Sub
    <TearDown()> _
    Public Sub Clean()
    End Sub


#Region "ChDir"

    <Test()> _
    Public Sub ChDir_1()

        Dim test_dir As String = "chdir_test1"
        Dim cur_dir, tmpStr As String
        Dim last_ch As Integer

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        FileSystem.ChDir(DATA_DIR + sep_ch + test_dir)
        tmpStr = Directory.GetCurrentDirectory()
        last_ch = tmpStr.LastIndexOf(sep_ch)
        cur_dir = tmpStr.Substring(last_ch + 1, (tmpStr.Length - last_ch) - 1)

        Assert.AreEqual(cur_dir, test_dir)
        FileSystem.ChDir("..")
        Directory.Delete(DATA_DIR + sep_ch + test_dir)
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub ChDir_2()
        FileSystem.ChDir("")
    End Sub
    <Test(), ExpectedException(GetType(DirectoryNotFoundException))> _
    Public Sub ChDir_3()
        Dim test_dir As String = "chdir_test3"
        FileSystem.ChDir(test_dir)
    End Sub
#End Region

#Region "CurDir"

    <Test()> _
    Public Sub CurDir_1()

        Dim cur_dir, test_dir As String
        Dim bRes As Boolean = True
        FileSystem.ChDir(DATA_DIR)
        cur_dir = Directory.GetCurrentDirectory()
        test_dir = FileSystem.CurDir()
        Assert.AreEqual(cur_dir, test_dir)
        Directory.CreateDirectory("CurDir_1")
        FileSystem.ChDir(DATA_DIR + sep_ch + "CurDir_1")
        test_dir = FileSystem.CurDir()

        If (cur_dir = test_dir) Then bRes = False
        Assert.AreEqual(True, bRes)
        FileSystem.ChDir("..")
        Directory.Delete(DATA_DIR + sep_ch + "CurDir_1")
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub CurDir_2()
        FileSystem.CurDir("2")
    End Sub
    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub CurDir_3()
        '' hopefully J won`t exist on this computer
        Dim test_drive As Char = "J"c
        FileSystem.CurDir(test_drive)
    End Sub
#End Region

#Region "ChDrive"

    <Test()> _
    Public Sub ChDrive_1()
        Dim test_drive As Char = "C"c
        Dim cur_drive As Char
        Dim tmpStr As String

        FileSystem.ChDrive(test_drive)
        tmpStr = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())
        cur_drive = tmpStr.Substring(0, 1)
        Assert.AreEqual(cur_drive, test_drive)
    End Sub
    <Test()> _
    Public Sub ChDrive_2()
        Dim test_drive As String = ""
        Dim cur_drive, last_drive As Char
        Dim tmpStr As String
        tmpStr = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())
        last_drive = tmpStr.Substring(0, 1)
        FileSystem.ChDrive(test_drive)
        tmpStr = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())
        cur_drive = tmpStr.Substring(0, 1)
        Assert.AreEqual(cur_drive, last_drive)
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub ChDrive_3()
        FileSystem.ChDrive("2")
    End Sub
    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub ChDrive_4()
        FileSystem.ChDrive("TR:\")
    End Sub

#End Region

#Region "FileCopy"

    <Test()> _
    Public Sub FileCopy_1()
        Dim dest_dir As String = "temp_dir1"
        Dim src_file As String = "FileCopy_1.txt"
        Dim fs As FileStream


        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)
        fs = File.Create(DATA_DIR + sep_ch + src_file)
        fs.Close()

        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))
        Directory.CreateDirectory(CStr(DATA_DIR + sep_ch + dest_dir))

        FileSystem.FileCopy(DATA_DIR + sep_ch + src_file, DATA_DIR + sep_ch + dest_dir + sep_ch + src_file)
        ' wait a while till the copy ends
        Thread.Sleep(100)
        Assert.AreEqual(True, File.Exists(DATA_DIR + sep_ch + dest_dir + sep_ch + src_file))

        File.Delete(DATA_DIR + sep_ch + dest_dir + sep_ch + src_file)
        Directory.Delete(DATA_DIR + sep_ch + dest_dir, True)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub FileCopy_2()
        Dim dest_dir As String = "temp_dir2"
        Dim src_file As String = "FileCopy_2.txt"

        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)
        File.CreateText(DATA_DIR + sep_ch + src_file)

        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))
        Directory.CreateDirectory(CStr(DATA_DIR + sep_ch + dest_dir))

        '' pass null src
        FileSystem.FileCopy("", DATA_DIR + sep_ch + dest_dir)

    End Sub
    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileCopy_3()
        Dim dest_dir As String = "temp_dir3"
        Dim src_file As String = "FileCopy_3.txt"

        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)
        File.CreateText(DATA_DIR + sep_ch + src_file)

        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))
        Directory.CreateDirectory(CStr(DATA_DIR + sep_ch + dest_dir))

        '' pass existing directory name to copy
        FileSystem.FileCopy(DATA_DIR + sep_ch + src_file, DATA_DIR + sep_ch + dest_dir)

        File.Delete(DATA_DIR + sep_ch + src_file)
        File.Delete(DATA_DIR + sep_ch + dest_dir)
        File.Delete(DATA_DIR + sep_ch + dest_dir + sep_ch + src_file)
        Directory.Delete(DATA_DIR + sep_ch + dest_dir)
    End Sub
    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub FileCopy_4()
        Dim dest_dir As String = "temp_dir4"
        Dim src_file As String = "FileCopy_4.txt"

        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)
        File.CreateText(DATA_DIR + sep_ch + src_file)

        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))
        Directory.CreateDirectory(CStr(DATA_DIR + sep_ch + dest_dir))

        '' pass null destination
        FileSystem.FileCopy(DATA_DIR + sep_ch + dest_dir, "")

    End Sub
    <Test(), ExpectedException(GetType(FileNotFoundException))> _
     Public Sub FileCopy_5()

        Dim dest_dir As String = "temp_dir5"
        Dim src_file As String = "FileCopy_5.txt"

        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)


        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))
        Directory.CreateDirectory(CStr(DATA_DIR + sep_ch + dest_dir))

        '' pass missing src file
        FileSystem.FileCopy(DATA_DIR + sep_ch + src_file, DATA_DIR + sep_ch + dest_dir + sep_ch + src_file)

    End Sub

    <Test(), ExpectedException(GetType(DirectoryNotFoundException))> _
    Public Sub FileCopy_6()

        Dim dest_dir As String = "temp_dir6"
        Dim src_file As String = "FileCopy_6.txt"

        If File.Exists(DATA_DIR + sep_ch + src_file) Then File.Delete(DATA_DIR + sep_ch + src_file)
        File.CreateText(DATA_DIR + sep_ch + src_file)


        If Directory.Exists(CStr(DATA_DIR + sep_ch + dest_dir)) Then Directory.Delete(CStr(DATA_DIR + sep_ch + dest_dir))


        '' pass missing directory to destination
        FileSystem.FileCopy(DATA_DIR + sep_ch + src_file, DATA_DIR + sep_ch + dest_dir + sep_ch + src_file)

    End Sub
#End Region

#Region "FileDateTime"

    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
#If Not TARGET_JVM Then
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub FileDateTime_1()

        Dim test_file As String = "FileDateTime_test1.dat"
        Dim create_time, test_time As Date
        Dim fs As FileStream

        fs = File.Create(DATA_DIR + sep_ch + test_file)

        create_time = File.GetCreationTime(DATA_DIR + sep_ch + test_file)
        test_time = FileSystem.FileDateTime(DATA_DIR + sep_ch + test_file)
        Assert.AreEqual(create_time, test_time, "Creation Time should be the same")
        fs.Close()
        Thread.Sleep(600)
        File.Delete(DATA_DIR + sep_ch + test_file)
    End Sub
#End If

#If Not TARGET_JVM Then
    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub FileDateTime_2()

        Dim test_file As String = "FileDateTime_test1.dat"
        Dim modify_time, test_time As Date
        Dim fs As FileStream

        fs = File.Create(DATA_DIR + sep_ch + test_file)
        Thread.Sleep(60)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
        ' Add some information to the file.
        fs.Write(info, 0, info.Length)
        fs.Close()
        modify_time = File.GetLastAccessTime(DATA_DIR + sep_ch + test_file)
        test_time = FileSystem.FileDateTime(DATA_DIR + sep_ch + test_file)
        Assert.AreEqual(modify_time, test_time, "Modification Time should be the same")
        Thread.Sleep(60)
        File.Delete(DATA_DIR + sep_ch + test_file)
    End Sub
#End If

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub FileDateTime_3()
        FileSystem.FileDateTime("")
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub FileDateTime_4()
        FileSystem.FileDateTime("c:\test_wrong_path\?")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub FileDateTime_5()
        Dim test_file As String = "FileDateTime_test3.txt"
        FileSystem.FileDateTime(test_file)
    End Sub
#End Region

#Region "FileLen"

    <Test()> _
    Public Sub FileLen_1()

        Dim test_file As String = "FileLen_test1.dat"
        Dim file_len As Integer
        Dim fs As FileStream

        fs = File.Create(DATA_DIR + sep_ch + test_file)
        Thread.Sleep(60)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
        ' Add some information to the file.
        fs.Write(info, 0, info.Length)
        fs.Close()

        file_len = FileSystem.FileLen(DATA_DIR + sep_ch + test_file)
        Assert.AreEqual(30, file_len)
        Thread.Sleep(60)
        File.Delete(DATA_DIR + sep_ch + test_file)
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub FileLen_2()
        FileSystem.FileLen("")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub FileLen_3()
        Dim test_dir As String = "FileLen_test3.dat"
        FileSystem.FileLen(test_dir)
    End Sub
#End Region

#Region "GetAttr"

    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub GetAttr_1()

        '' check attr to file
        Dim test_file As String = "GetAttr_test1.dat"
        Dim file_attr As FileAttribute, req_attr As FileAttributes
        Dim fs As FileStream
        req_attr = FileAttributes.Hidden Or FileAttributes.Archive
        fs = File.Create(DATA_DIR + sep_ch + test_file)
        Thread.Sleep(60)
        fs.Close()
        File.SetAttributes(DATA_DIR + sep_ch + test_file, req_attr)

        file_attr = FileSystem.GetAttr(DATA_DIR + sep_ch + test_file)
        Assert.AreEqual(req_attr.ToString(), file_attr.ToString())
        Thread.Sleep(60)
        File.Delete(DATA_DIR + sep_ch + test_file)
    End Sub

    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub GetAttr_2()
        '' check attr to directory
        Dim test_dir As String = "GetAttr_Dirtest2"
        Dim file_attr As FileAttribute, req_attr As FileAttributes

        req_attr = FileAttributes.Hidden Or FileAttributes.Archive Or FileAttributes.Directory
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)

        File.SetAttributes(DATA_DIR + sep_ch + test_dir, req_attr)

        file_attr = FileSystem.GetAttr(DATA_DIR + sep_ch + test_dir)
        Assert.AreEqual(CObj(req_attr), CObj(file_attr))
        Thread.Sleep(60)
        Directory.Delete(DATA_DIR + sep_ch + test_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub GetAttr_3()
        FileSystem.GetAttr("")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub GetAttr_4()
        Dim test_dir As String = "GetAttr_test4.dat"
        FileSystem.GetAttr(test_dir)
    End Sub
#End Region

#Region "Kill"

    <Test()> _
    Public Sub Kill_1()

        '' kill one file
        Dim test_file1 As String = "Kill_1_test1.dat"
        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file1, 1024)

        fs.Close()

        FileSystem.Kill(DATA_DIR + sep_ch + test_file1)
        Thread.Sleep(60)
        Assert.AreEqual(False, File.Exists(DATA_DIR + sep_ch + test_file1))
    End Sub

    <Test()> _
    Public Sub Kill_2()

        '' kill files with *.dat
        Dim test_file1 As String = "Kill_2_test1.dat"
        Dim test_file2 As String = "Kill_2_test2.dat"
        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file1, 1024)
        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_file2, 1024)

        fs.Close()
        fs1.Close()

        FileSystem.Kill(DATA_DIR + sep_ch + "Kill*.dat")
        Thread.Sleep(60)
        Assert.AreEqual(False, File.Exists(DATA_DIR + sep_ch + test_file1), "kill need to remove also Kill_2_test1.dat")
        Assert.AreEqual(False, File.Exists(DATA_DIR + sep_ch + test_file2), "kill need to remove also Kill_2_test2.dat")

    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub Kill_3()
        '' try to kill a directory
        Dim test_dir As String = "Kill_Dirtest2"
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        FileSystem.Kill(DATA_DIR + sep_ch + test_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Kill_4()
        FileSystem.Kill("")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub Kill_5()
        Dim test_dir As String = "Kill_test4.dat"
        FileSystem.Kill(test_dir)
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Kill_6()
        '' * in the path
        Dim test_dir As String = "Kill_test4.dat"
        FileSystem.Kill(DATA_DIR + "*" + sep_ch + test_dir)
    End Sub
#End Region

#Region "MKDir"

    <Test()> _
    Public Sub MKDir_1()

        Dim test_dir As String = "MKDir_test1"

        FileSystem.MkDir(DATA_DIR + sep_ch + test_dir)
        Dim dirinfo As New DirectoryInfo(DATA_DIR + sep_ch + test_dir)

        Assert.AreEqual(True, dirinfo.Exists)

        Directory.Delete(DATA_DIR + sep_ch + test_dir)
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub MKDir_2()

        Dim test_dir As String = "MKDir_test2"

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        FileSystem.MkDir(DATA_DIR + sep_ch + test_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub MKDir_3()
        FileSystem.MkDir("")
    End Sub
    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub MKDir_4()
        Dim test_dir As String = "MKdir_t<est3"
        FileSystem.MkDir(DATA_DIR + sep_ch + test_dir)
    End Sub
#End Region

#Region "Rename"

    <Test()> _
    Public Sub Rename_1()
        '' rename directory
        Dim test_dir As String = "Rename_test1"
        Dim test_dir_new As String = "Rename_test1_new"

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        FileSystem.Rename(DATA_DIR + sep_ch + test_dir, DATA_DIR + sep_ch + test_dir_new)
        Dim dirinfo_new As New DirectoryInfo(DATA_DIR + sep_ch + test_dir_new)

        Assert.AreEqual(True, dirinfo_new.Exists)
        Thread.Sleep(60)
        Directory.Delete(DATA_DIR + sep_ch + test_dir_new)

    End Sub

    <Test()> _
    Public Sub Rename_2()
        '' rename file
        Dim test_file As String = "Rename_test2.txt"
        Dim test_file_new As String = "Rename_test2_new.dat"

        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file, 1024)
        fs.Close()

        FileSystem.Rename(DATA_DIR + sep_ch + test_file, DATA_DIR + sep_ch + test_file_new)
        Dim fileinfo_new As New FileInfo(DATA_DIR + sep_ch + test_file_new)

        Assert.AreEqual(True, fileinfo_new.Exists)

        File.Delete(DATA_DIR + sep_ch + test_file_new)
    End Sub

    <Test(), ExpectedException(GetType(IOException), "File already exists.")> _
    Public Sub Rename_3()
        Dim test_dir As String = "Rename_test3"
        Dim tmp_dir As String = "Try_Dir"
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        Directory.CreateDirectory(DATA_DIR + sep_ch + tmp_dir)

        FileSystem.Rename(DATA_DIR + sep_ch + test_dir, DATA_DIR + sep_ch + tmp_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Rename_4()
        FileSystem.Rename("", "Test")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
     Public Sub Rename_5()
        Dim test_dir As String = "Rename_test5"
        FileSystem.Rename("fff", test_dir)
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Rename_6()
        Dim test_file As String = "Rename_t<est6"
        Dim test_file2 As String = "Rename_test6"

        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file, 1024)
        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_file2, 1024)

        fs.Close()
        fs1.Close()

        FileSystem.Rename(DATA_DIR + sep_ch + test_file, DATA_DIR + sep_ch + test_file2)
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Rename_7()
        Dim test_file As String = "Rename_test7.dat"
        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file, 1024)

        FileSystem.Rename(test_file, "")
    End Sub
    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Rename_8()
        '' move a file to unavailable directory 
        Dim test_file As String = "Rename_test8.txt"
        Dim test_file_new As String = "Rename_test8_new.dat"

        Dim fs As FileStream = File.Create(DATA_DIR + sep_ch + test_file, 1024)
        fs.Close()

        FileSystem.Rename(DATA_DIR + sep_ch + test_file, DATA_DIR + sep_ch + "not_there" + sep_ch + test_file_new)
        Dim fileinfo_new As New FileInfo(DATA_DIR + sep_ch + test_file_new)

        Assert.AreEqual(False, fileinfo_new.Exists)

        File.Delete(DATA_DIR + sep_ch + test_file_new)
    End Sub

#End Region

#Region "SetAttr"

    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub SetAttr_1()

        '' check attr on file
        Dim test_file As String = "SetAttr_test1.dat"
        Dim file_attr As FileAttributes, req_attr As FileAttribute
        Dim fs As FileStream
        req_attr = vbHidden Or vbArchive
        fs = File.Create(DATA_DIR + sep_ch + test_file)
        Thread.Sleep(60)
        fs.Close()
        FileSystem.SetAttr(DATA_DIR + sep_ch + test_file, req_attr)

        file_attr = File.GetAttributes(DATA_DIR + sep_ch + test_file)

        Assert.AreEqual(CObj(req_attr), CObj(file_attr))
        Thread.Sleep(60)
        File.Delete(DATA_DIR + sep_ch + test_file)
    End Sub

    'TargetJvmNotSupported - File metadata/attributes feature is not supported 
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub SetAttr_2()
        '' check attr on directory
        Dim test_dir As String = "SetAttr_Dirtest2"
        Dim file_attr As FileAttribute, req_attr As FileAttribute

        req_attr = vbHidden Or vbArchive
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)

        FileSystem.SetAttr(DATA_DIR + sep_ch + test_dir, req_attr)

        file_attr = FileSystem.GetAttr(DATA_DIR + sep_ch + test_dir)

        Assert.AreEqual(CObj(req_attr Or vbDirectory), CObj(file_attr))
        Thread.Sleep(60)
        Directory.Delete(DATA_DIR + sep_ch + test_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub SetAttr_3()
        FileSystem.GetAttr("")
    End Sub

    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub SetAttr_4()
        Dim test_dir As String = "SetAttr_test4.dat"
        FileSystem.GetAttr(test_dir)
    End Sub
#End Region

#Region "RMDir"

    <Test()> _
    Public Sub RMDir_1()

        Dim test_dir As String = "RMDir_test1"

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        FileSystem.RmDir(DATA_DIR + sep_ch + test_dir)
        Dim dirinfo As New DirectoryInfo(DATA_DIR + sep_ch + test_dir)

        Assert.AreEqual(False, dirinfo.Exists)

    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub RMDir_2()
        '' directory not empty
        Dim test_dir As String = "RMDir_test2"
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_dir + sep_ch + "sss.txt", 1024)
        fs1.Close()

        FileSystem.RmDir(DATA_DIR + sep_ch + test_dir)

    End Sub

    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub RMDir_3()
        FileSystem.MkDir("")
    End Sub
    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub RMDir_4()
        Dim test_dir As String = "RMDir_t<est4"
        FileSystem.RmDir(DATA_DIR + sep_ch + test_dir)
    End Sub
    <Test(), ExpectedException(GetType(DirectoryNotFoundException))> _
    Public Sub RMDir_5()
        Dim test_dir As String = "RMDir_test5"
        FileSystem.RmDir(DATA_DIR + sep_ch + test_dir)
    End Sub
#End Region


#Region "DirTests"
    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Dir_1()
        Dim test_dir As String = "Dir_test1"
        FileSystem.Dir()
    End Sub
    <Test()> _
    Public Sub Dir_2()

        Dim test_dir As String = "Dir_test2"
        Dim res_dir As String

        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + test_dir)

        Assert.AreEqual("", res_dir)

    End Sub

    <Test()> _
    Public Sub Dir_3()

        Dim test_dir As String = "Dir_test3"
        Dim res_dir As String

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir)
        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + test_dir, FileAttribute.Directory)

        Assert.AreEqual(test_dir, res_dir)
        ''  Directory.Delete(DATA_DIR + sep_ch + test_dir)

    End Sub



    <Test()> _
    Public Sub Dir_4()

        Dim test_dir1 As String = "Dir_test4_1"
        Dim test_dir2 As String = "Dir_test4_2"
        Dim test_dir3 As String = "Dir_test4_3"
        Dim res_dir As String
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir1)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir2)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir3)


        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test4_?", FileAttribute.Directory)
        Assert.AreEqual(test_dir1, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir2, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir3, res_dir)



    End Sub

    <Test()> _
    Public Sub Dir_5()

        Dim test_dir1 As String = "Dir_test5_1"
        Dim test_dir2 As String = "Dir_test5_2"
        Dim res_dir As String
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir1)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir2)


        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test5_?", FileAttribute.Directory)
        Assert.AreEqual(test_dir1, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir2, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(Nothing, res_dir)  '' after last match retyrn null

    End Sub


    <Test(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Dir_6()

        Dim test_dir1 As String = "Dir_test5_1"
        Dim test_dir2 As String = "Dir_test5_2"
        Dim res_dir As String
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir1)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir2)


        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test5_?", FileAttribute.Directory)
        Assert.AreEqual(test_dir1, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir2, res_dir)
        res_dir = FileSystem.Dir()
        Assert.AreEqual(Nothing, res_dir)
        res_dir = FileSystem.Dir()  '' this one should raise an exception 

    End Sub

    <Test()> _
    Public Sub Dir_7()

        Dim test_file1 As String = "Dir_test_file7_1.check"
        Dim test_file2 As String = "Dir_test_file7_2.check"
        Dim res_file As String

        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_file1, 1024)
        fs1.Close()

        Dim fs2 As FileStream = File.Create(DATA_DIR + sep_ch + test_file2, 1024)
        fs2.Close()

        res_file = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test_file7_*.*")
        Assert.AreEqual(test_file1, res_file)
        res_file = FileSystem.Dir()
        Assert.AreEqual(test_file2, res_file)
        res_file = FileSystem.Dir()
        Assert.AreEqual(Nothing, res_file)  '' after last match return null

    End Sub



    <Test()> _
    Public Sub Dir_8()

        Dim test_file1 As String = "Dir_test_file8_1.check"
        Dim test_file2 As String = "Dir_test_file8_2.check"
        Dim res_file As String

        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_file1, 1024)
        fs1.Close()

        Dim fs2 As FileStream = File.Create(DATA_DIR + sep_ch + test_file2, 1024)
        fs2.Close()

        res_file = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test_file8_1.*")
        Assert.AreEqual(test_file1, res_file)
        res_file = FileSystem.Dir()
        Assert.AreEqual(Nothing, res_file)  '' after last match retyrn null

    End Sub


    <Test()> _
    Public Sub Dir_9()

        Dim test_file1 As String = "Dir_test_file9_1.check"
        Dim res_file As String

        res_file = FileSystem.Dir("c:\tes^~~^^^\Dir_test_file9_1.*")
        Assert.AreEqual("", res_file)
    End Sub


    <Test()> _
    Public Sub Dir_10()
        Dim res_file As String
        Dim test_dir1 As String = "Dir_test_10"

        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir1)
        Dim test_file1 As String = "Dir_test_11_file_1.check"
        Dim test_file2 As String = "Dir_test_11_file_2.txt"

        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_dir1 + sep_ch + test_file1, 1024)
        fs1.Close()

        Dim fs2 As FileStream = File.Create(DATA_DIR + sep_ch + test_dir1 + sep_ch + test_file2, 1024)
        fs2.Close()


        FileSystem.ChDir(DATA_DIR + sep_ch + test_dir1)

        res_file = FileSystem.Dir("") '' returns all files with normal attr
        Assert.AreEqual(test_file1, res_file)
        res_file = FileSystem.Dir()
        Assert.AreEqual(test_file2, res_file)

    End Sub


    <Test()> _
    Public Sub Dir_11()

        Dim test_dir1 As String = "Dir_test_11_1"
        Dim test_dir2 As String = "Dir_test_11_2"
        Dim test_dir3 As String = "Dir_test_11_3"
        Dim res_dir As String
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir1)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir2)
        Directory.CreateDirectory(DATA_DIR + sep_ch + test_dir3)
        Dim test_file1 As String = "Dir_test_11_file_1.check"
        Dim test_file2 As String = "Dir_test_11_file_2.check"

        Dim fs1 As FileStream = File.Create(DATA_DIR + sep_ch + test_file1, 1024)
        fs1.Close()

        Dim fs2 As FileStream = File.Create(DATA_DIR + sep_ch + test_file2, 1024)
        fs2.Close()

        res_dir = FileSystem.Dir(DATA_DIR + sep_ch + "Dir_test_11*", FileAttribute.Directory) '' all files and dirs
        Assert.AreEqual(test_dir1, res_dir, "1")
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir2, res_dir, "2")
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_dir3, res_dir, "3")
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_file1, res_dir, "4")
        res_dir = FileSystem.Dir()
        Assert.AreEqual(test_file2, res_dir, "5")

    End Sub
#End Region

End Class