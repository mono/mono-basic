'
' FileSystemTests2.vb - NUnit Test Cases for Microsoft.VisualBasic.FileSystem 
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
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
'
Imports NUnit.Framework
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Threading
Imports Microsoft.VisualBasic
', Ignore("Not implemented yet")> _
<Category("Broken"), TestFixture(), Category("MayFailOnSharedDrived")> _
Public Class FileSystemTests2
    Private DATA_DIR As String

    Sub New()
        DATA_DIR = Environment.GetEnvironmentVariable("DATA_DIR")
        If DATA_DIR Is Nothing OrElse DATA_DIR = String.Empty Then
            DATA_DIR = Path.Combine(Path.GetTempPath, "FileSystemTests2")
        End If
    End Sub

    Private Function JoinBytes(ByVal Bytes() As Byte) As String
        Dim result As New StringBuilder

        For i As Integer = 0 To Bytes.Length - 1
            result.Append(Hex(Bytes(i)))
            If i < Bytes.Length - 1 Then
                result.Append("-"c)
            End If
        Next

        Return result.ToString
    End Function


    Private Sub Initialize(ByVal FilenameToCreate As String, ByVal Contents As Byte())
        Initialize()
        Helper.WriteAllBytes(FilenameToCreate, Contents)
    End Sub

    Private Sub Initialize(ByVal FilenameToCreate As String, ByVal Contents As String)
        Initialize()
        Helper.WriteAllText(FilenameToCreate, Contents)
    End Sub

    Private Sub Initialize(ByVal FilenameToCreate As String)
        Initialize()
        Helper.WriteAllBytes(FilenameToCreate, New Byte() {})
    End Sub

    Private Sub Initialize()
        If Not Directory.Exists(DATA_DIR) Then
            Directory.CreateDirectory(DATA_DIR)
        End If
    End Sub

    Private Sub CleanUp()
        'Close all files and delete them
        Microsoft.VisualBasic.FileSystem.FileClose()
        If Directory.Exists(DATA_DIR) Then
            Directory.Delete(DATA_DIR, True)
        End If
    End Sub

#Region "FreeFile"
    <Test()> _
    Public Sub FreeFileTest1()
        'First free file# is 1
        'This test may fail if there are open files.
        'Besides, there are no guarantees that the first file# will be 1
        Assert.AreEqual(1, FreeFile, "#01")
    End Sub

    <Test(), ExpectedException(GetType(IOException), "Too many files.")> _
    Public Sub FreeFileTest2()
        Dim openedAll As Boolean
        Try
            'Use up all the file numbers
            For i As Integer = 1 To 255
                FileOpen(i, System.Reflection.Assembly.GetExecutingAssembly.Location, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            Next
            openedAll = True
            FreeFile() 'This should cause the exception
        Finally
            CleanUp()
            Assert.IsTrue(openedAll, "Opened all files")
        End Try
    End Sub
#End Region

#Region "Array tests"
    <Test()> _
    Public Sub BinaryPutArrayTest1()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Dim value As String = Nothing
            Dim arr As Array = New Integer() {1, 2, 3}
            Initialize()
            FileOpen(1, filename, OpenMode.Binary)
            FilePut(1, arr, , False)
            FileClose(1)
            'Debug.WriteLine(Helper.CreateCode(File.ReadAllBytes(filename)))
            Helper.CompareBytes(New Byte() {1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0}, Helper.ReadAllBytes(filename), filename)
        Finally
            CleanUp()
        End Try
    End Sub
    <Test()> _
    Public Sub BinaryPutArrayTest2()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Dim value As String = Nothing
            Dim arr As Array = New Integer() {1, 2, 3}
            Initialize()
            FileOpen(1, filename, OpenMode.Binary)
            FilePut(1, arr, , True)
            FileClose(1)
            Helper.CompareBytes(New Byte() {1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0}, Helper.ReadAllBytes(filename), filename)
        Finally
            CleanUp()
        End Try
    End Sub
    <Test()> _
    Public Sub RandomPutArrayTest1()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Dim value As String = Nothing
            Dim arr As Array = New Integer() {1, 2, 3}
            Initialize()
            FileOpen(1, filename, OpenMode.Random)
            FilePut(1, arr, , False)
            FileClose(1)
            Helper.CompareBytes(New Byte() {1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0}, Helper.ReadAllBytes(filename), filename)
        Finally
            CleanUp()
        End Try
    End Sub
    <Test()> _
    Public Sub RandomPutArrayTest2()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Dim value As String = Nothing
            Dim arr As Array = New Integer() {1, 2, 3}
            Initialize()
            FileOpen(1, filename, OpenMode.Random)
            FilePut(1, arr, , True)
            FileClose(1)
            Helper.CompareBytes(New Byte() {1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0}, Helper.ReadAllBytes(filename), filename)
        Finally
            CleanUp()
        End Try
    End Sub
#End Region

    <Test()> _
    Sub InputStringWeirdTest1()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Dim value As String = Nothing
            Initialize()
            Helper.WriteAllBytes(filename, New Byte() {255, 255})
            FileOpen(1, filename, OpenMode.Append)
            value = InputString(1, 2)
            FileClose(1)
            Assert.Fail("Expected System.NullReferenceException ('Object reference not set to an instance of an object.')", filename)
        Catch ex As NUnit.Framework.AssertionException
            Throw
        Catch ex As Exception
            Assert.AreEqual("System.NullReferenceException", ex.GetType.FullName, filename)
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message, filename)
            'NRE with InnerException??
            Assert.IsNotNull(ex.InnerException, filename)
            Assert.AreSame(GetType(IOException), ex.InnerException.GetType, filename)
            Assert.AreEqual("File is not opened for read access.", ex.InnerException.Message, filename)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Sub RandomDefaultRecordLength()
        Dim filename As String = Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        For i As Integer = 0 To 128
            Try
                Dim value As String = New String("-", 129)
                Initialize()
                Helper.WriteAllBytes(filename, New Byte() {i, 0})
                Helper.AppendAllText(filename, New String("-"c, 128))
                FileOpen(1, filename, OpenMode.Random)
                FileGet(1, value)
                Assert.IsTrue(i < 127, filename)
                FileClose(1)
            Catch ex As NUnit.Framework.AssertionException
                Throw
            Catch ex As Exception
                Assert.IsTrue(i >= 127, filename)
                Assert.AreEqual("System.IO.IOException", ex.GetType.FullName, filename)
                Assert.AreEqual("Bad record length.", ex.Message, filename)
            Finally
                CleanUp()
            End Try
        Next
    End Sub

    Public Sub SeekTest1()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Default, OpenShare.Default)
            Seek(1, -1)
            Assert.AreEqual(1, Seek(1), "#01")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    Public Sub SeekTest2()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Default, OpenShare.Default)
            Seek(1, 0)
            Assert.AreEqual(1, Seek(1), "#01")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    Public Sub SeekTest3()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Default, OpenShare.Default)
            Seek(1, 2)
            Assert.AreEqual(2, Seek(1), "#01")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub
#Region "FileOpen"
    <Test()> _
    Public Sub FileOpenTestRandom1()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Default access and share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Default, OpenShare.Shared)
            data = "abc"
            FilePut(1, data) 'We can write
            FileGet(1, data) 'We can read
            Assert.AreEqual("", data, "#01")
            Seek(1, 1)
            FileGet(1, data)
            Assert.AreEqual("abc", data, "#02")
            FileClose(1)
            Helper.CompareBytes(New Byte() {3, 0, 97, 98, 99}, Helper.ReadAllBytes(filename), filename)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileOpenTestRandom1_Locked()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Default access and share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Default, OpenShare.Default)
            data = "abc"
            FilePut(1, data)
            data = Helper.ReadAllText(filename) 'Default share is none at all, so this should throw an exception
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestRandom2()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Read access and default share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Read, OpenShare.Default)
            data = "abc"
            FileGet(1, data) 'We can read
            Assert.AreEqual("", data, "#01")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "File is not opened for write access.")> _
    Public Sub FileOpenTestRandom2_b()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Read access and default share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Read, OpenShare.Default)
            data = "abc"
            FileGet(1, data) 'We can read
            FilePut(1, data) 'We can't write
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestRandom3()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Write access and default share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Write, OpenShare.Default)
            data = "abc"
            FilePut(1, data) 'We can write
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "File is not opened for read access.")> _
    Public Sub FileOpenTestRandom3_b()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Write access and default share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.Write, OpenShare.Default)
            data = "abc"
            FilePut(1, data) 'We can write
            FileGet(1, data) 'We can't read
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestRandom4()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Random, OpenAccess.ReadWrite, OpenShare.Default)
            data = "abc"
            FilePut(1, data) 'We can write
            FileGet(1, data) 'We can read
            Assert.AreEqual("", data, "#01")
            Seek(1, 1)
            FileGet(1, data)
            Assert.AreEqual("abc", data, "#02")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub


    <Test()> _
    Public Sub FileOpenTestInput1()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Default, OpenShare.Default)
            data = LineInput(1) 'We can read
            Assert.AreEqual("abc", data, "#01")
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileOpenTestInput1_Locked()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Default, OpenShare.Default)
            data = Helper.ReadAllText(filename) 'Default share is none at all, so this should throw an exception
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestInput2()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Read access and default share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Read, OpenShare.Default)
            data = LineInput(1) 'We can read
            Assert.AreEqual("abc", data, "#01")
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "Bad file mode.")> _
    Public Sub FileOpenTestInput2_b()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "bcd")

            'Read access and default share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Read, OpenShare.Default)
            Write(1, "abc") 'We can't write
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException), "Argument 'Access' is not valid. Valid values for Input mode are 'OpenAccess.Read' and 'OpenAccess.Default'.")> _
    Public Sub FileOpenTestInput3()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename)

            'Write access and default share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Write, OpenShare.Default)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException), "Argument 'Access' is not valid. Valid values for Input mode are 'OpenAccess.Read' and 'OpenAccess.Default'.")> _
    Public Sub FileOpenTestInput4()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename)

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.ReadWrite, OpenShare.Default)
        Finally
            CleanUp()
        End Try
    End Sub


    <Test(), ExpectedException(GetType(FileNotFoundException))> _
    Public Sub FileOpenTestInput5()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize() 'Not creating the file here

            'Default access and share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Default, OpenShare.Default)
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "Bad file mode.")> _
    Public Sub FileOpenTestInput6()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Default, OpenShare.Default)
            FileGet(1, data) 'We can't use FileGet for sequential files
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestOutput1()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Default access and share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.Default, OpenShare.Default)
            Assert.IsTrue(File.Exists(filename), "exists")
            data = "abc"
            Print(1, data) 'We can write
            'Seek(1, 1)
            'data = LineInput(1) 'We can read
            'Assert.AreEqual("abc", data, "#01")
            FileClose(1)
            Assert.AreEqual("abc", Helper.ReadAllText(filename))
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileOpenTestOutput1_Locked()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Default access and share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.Default, OpenShare.Default)
            Assert.IsTrue(File.Exists(filename), "exists")
            data = "abc"
            Write(1, data)
            data = Helper.ReadAllText(filename) 'Default share is none at all, so this should throw an exception
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestOutput2()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Read access and default share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.Write, OpenShare.Default)
            Assert.IsTrue(File.Exists(filename), "exists")
            data = "abc"
            Write(1, data) 'We can write
            FileClose(1)
            Assert.AreEqual("""abc"",", Helper.ReadAllText(filename))
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "Bad file mode.")> _
    Public Sub FileOpenTestOutput2_b()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Read access and default share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.Write, OpenShare.Default)
            Assert.IsTrue(File.Exists(filename), "exists")
            data = "abc"
            Write(1, data) 'We can write
            data = LineInput(1) 'We can't read
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException), "Argument 'Access' is not valid. Valid values for Output mode are 'OpenAccess.Write' and 'OpenAccess.Default'.")> _
    Public Sub FileOpenTestOutput3()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'Write access and default share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.Read, OpenShare.Default)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException), "Argument 'Access' is not valid. Valid values for Output mode are 'OpenAccess.Write' and 'OpenAccess.Default'.")> _
    Public Sub FileOpenTestOutput4()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize()

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Output, OpenAccess.ReadWrite, OpenShare.Default)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestAppend1()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.Default, OpenShare.Default)
            Print(1, "def") 'We can write
            FileClose(1)
            Assert.AreEqual("abcdef", Helper.ReadAllText(filename), "#01")
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileOpenTestAppend1_Locked()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.Default, OpenShare.Default)
            data = Helper.ReadAllText(filename) 'Default share is none at all, so this should throw an exception
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestAppend2()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Write access and default share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.Write, OpenShare.Default)
            Print(1, "def") 'We can write
            FileClose(1)
            Assert.AreEqual("abcdef", Helper.ReadAllText(filename), "#01")
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(ArgumentException), "Argument 'Access' is not valid. Valid values for Append mode are 'OpenAccess.Write' and 'OpenAccess.Default'.")> _
    Public Sub FileOpenTestAppend3()
        Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
        Try
            Initialize(filename)

            'Read access and default share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.Read, OpenShare.Default)
            Assert.Fail("Expected System.ArgumentException ('Argument 'Access' is not valid. Valid values for Append mode are 'OpenAccess.Write' and 'OpenAccess.Default'.')", filename)
        Catch ex As NUnit.Framework.AssertionException
            Throw
        Catch ex As Exception
            Assert.AreEqual("System.ArgumentException", ex.GetType.FullName, filename)
            Assert.AreEqual("Argument 'Access' is not valid. Valid values for Append mode are 'OpenAccess.Write' and 'OpenAccess.Default'.", ex.Message.Replace("Acess", "Access"), filename)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestAppend4()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.ReadWrite, OpenShare.Default)
            Seek(1, 1)
            Print(1, "def") 'We can write
            FileClose(1)
            Assert.AreEqual("def", Helper.ReadAllText(filename), "#01")
        Finally
            CleanUp()
        End Try
    End Sub


    <Test()> _
    Public Sub FileOpenTestAppend5()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize() 'Not creating the file here

            'Default access and share
            FileOpen(1, filename, OpenMode.Append, OpenAccess.Default, OpenShare.Default)
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "Bad file mode.")> _
    Public Sub FileOpenTestAppend6()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Input, OpenAccess.Default, OpenShare.Default)
            FileGet(1, data) 'We can't use FileGet for sequential files
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestBinary1()
        Try
            Dim data As String = Nothing
            Dim b As Byte
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Default, OpenShare.Default)
            FileGet(1, b) 'We can read
            Assert.AreEqual(Asc("a"), b, "#01")
            FilePut(1, "def") 'We can write
            FileClose(1)
            Assert.AreEqual("adef", Helper.ReadAllText(filename), "#02")
            Assert.AreEqual("61-64-65-66", JoinBytes(Helper.ReadAllBytes(filename)), "#03")
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException))> _
    Public Sub FileOpenTestBinary1_Locked()
        Try
            Dim data As String
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Default, OpenShare.Default)
            data = Helper.ReadAllText(filename) 'Default share is none at all, so this should throw an exception
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestBinary2()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.Default)
            FileGet(1, data) 'We can read
            Assert.AreEqual(Nothing, data, "#01")
            FilePut(1, "abc") 'We can write
            Seek(1, 2) 'We can seek
            FilePut(1, "ghi")
            FileClose(1)
            Assert.AreEqual("aghi", Helper.ReadAllText(filename), "#02")
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "File is not opened for write access.")> _
    Public Sub FileOpenTestBinary2_b()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "bcd")

            'Read access and default share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Default)
            FilePut(1, data) 'We can't write
        Finally
            CleanUp()
        End Try
    End Sub

    <Test(), ExpectedException(GetType(IOException), "File is not opened for read access.")> _
    Public Sub FileOpenTestBinary3()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename)

            'Write access and default share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Write, OpenShare.Default)
            FileGet(1, data) 'We can't read
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestBinary4()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename)

            'R/W access and default share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.Default)
            FileGet(1, data) 'We can read
            Assert.AreEqual(Nothing, data, "#01")
            Seek(1, 3) 'We can seek
            FilePut(1, "abc") 'We can write
            Seek(1, 1) 'We can seek
            FilePut(1, "ghi")
            FileClose(1)
            Assert.AreEqual("ghibc", Helper.ReadAllText(filename), "#02")
        Finally
            CleanUp()
        End Try
    End Sub


    <Test()> _
    Public Sub FileOpenTestBinary5()
        Try
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize() 'Not creating the file here

            'Default access and share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Default, OpenShare.Default)
            FileClose(1)
        Finally
            CleanUp()
        End Try
    End Sub

    <Test()> _
    Public Sub FileOpenTestBinary6()
        Try
            Dim data As String = Nothing
            Dim filename As String = IO.Path.Combine(DATA_DIR, System.Reflection.MethodInfo.GetCurrentMethod.Name)
            Initialize(filename, "abc")

            'Default access and share
            FileOpen(1, filename, OpenMode.Binary, OpenAccess.Default, OpenShare.Default)
            data = LineInput(1) 'We can use (Line)Input for binary files
        Finally
            CleanUp()
        End Try
    End Sub
#End Region
End Class
