' NetworkTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Network
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

#If Not TARGET_JVM Then

Imports Microsoft.VisualBasic.Devices
Imports System.Net.NetworkInformation
Imports FS = Microsoft.VisualBasic.FileIO.FileSystem
Imports System.IO

Namespace Devices
    <TestFixture()> _
    Public Class NetworkTests
        Private BASEDIR As String
        Private SLOWFILESIZE As Integer = 10000000

        Sub New()

        End Sub

        <SetUp()> _
        Public Sub Init()
            BASEDIR = Path.Combine(Environment.CurrentDirectory, "NetworkTestCache").TrimEnd(New Char() {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar}) & Path.DirectorySeparatorChar
            If FS.DirectoryExists(BASEDIR) Then
                FS.DeleteDirectory(BASEDIR, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            FS.CreateDirectory(BASEDIR)
        End Sub

        <TearDown()> _
        Public Sub CleanUp()
            FS.DeleteDirectory(BASEDIR, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        Private Function ConnectedToInternet() As Boolean
            Try
                Return New Ping().Send("www.google.com").Status = IPStatus.Success
            Catch
                Return False
            End Try
        End Function

        <Test()> _
        Public Sub IsAvailableTest()
            Dim testname As String = "IsAvailableTest"
            Dim nw As New Network()

            Assert.AreEqual(NetworkInterface.GetIsNetworkAvailable(), nw.IsAvailable, testname & "-1")

        End Sub

        <Category("InternetRequired")> _
        <Category("Slow")> _
        <Test()> _
        Public Sub PingTest()
            Dim testname As String = "PingTest"
            Dim nw As New Network()

            If Not ConnectedToInternet() Then
                Assert.Ignore("No internet connection, so skipping ping tests")
                Return
            End If

            Dim realHost As String = "www.google.com"
            Dim imaginaryHost As String = "www.changemeifiexist.com"

            Assert.AreEqual(True, nw.Ping(realHost), testname & "-12")
            Assert.AreEqual(True, nw.Ping(realHost, 10000), testname & "-13")
            Assert.AreEqual(True, nw.Ping(New Uri("http://" & realHost)), testname & "-14")
            Assert.AreEqual(True, nw.Ping(New Uri("http://" & realHost), 10000), testname & "-15")

            Assert.AreEqual(False, nw.Ping(imaginaryHost), testname & "-22")
            Assert.AreEqual(False, nw.Ping(imaginaryHost, 10000), testname & "-23")
            Assert.AreEqual(False, nw.Ping(New Uri("http://" & imaginaryHost)), testname & "-24")
            Assert.AreEqual(False, nw.Ping(New Uri("http://" & imaginaryHost), 10000), testname & "-25")

        End Sub

        <Category("Slow")> _
        <Test()> _
        <Category("UI")> _
        Public Sub DownloadFileTest1()
            Dim testname As String = "DownloadFileTest1"
            Dim nw As New Network()

            If Not ConnectedToInternet() Then
                Assert.Ignore("No internet connection, so skipping download tests")
                Return
            End If

            Assert.Ignore("These tests are not implemented fully yet anyway")

            Dim smallrealFile As String = "http://www.google.com"
            Dim bigrealfile As String = "http://www.mono-project.com/"
            Dim imaginaryFile As String = "http://www.changemeifiexist.nowhere/nofile"

            Dim destination As String = Path.Combine(BASEDIR, "destination")
            Dim localsmallrealfile As String = Path.Combine(BASEDIR, "localsmallrealfile")
            Dim localbigrealfile As String = Path.Combine(BASEDIR, "localbigrealfile")

            Dim pwd As String
            Dim user As String
            Dim showUI As Boolean
            Dim timeout As Integer
            Dim onCancel As Microsoft.VisualBasic.FileIO.UICancelOption
            Dim i As Integer
            Dim overwrite As Boolean

            Using client As New Net.WebClient()
                client.DownloadFile(smallrealFile, localsmallrealfile)
                client.DownloadFile(bigrealfile, localbigrealfile)
            End Using


            '-----------------------------
            i += 1
            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-01")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination)
            Helper.CompareFile(destination, localbigrealfile, testname & "-02")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination)
                Assert.Fail(testname & "-03 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-04")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination, pwd, user)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = "pwd"
            user = "user"
            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination, pwd, user)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user)
                Assert.Fail(testname & "-" & i & "03 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = False

            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True

            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True

            nw.DownloadFile(smallrealFile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing

            IO.File.Delete(destination)
            nw.DownloadFile(smallrealFile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            Try
                nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "1" & "?")
            Catch ex As OperationCanceledException
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "2")
            End Try

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            nw.DownloadFile(bigrealfile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryFile, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try
        End Sub

        <Category("Slow")> _
        <Test()> _
        <Category("UI")> _
            Public Sub DownloadFileTest2()
            Dim testname As String = "DownloadFileTest2"
            Dim nw As New Network()

            If Not ConnectedToInternet() Then
                Assert.Ignore("No internet connection, so skipping download tests")
                Return
            End If

            Assert.Ignore("These tests are not implemented fully yet anyway")

            Dim smallrealFile As String = "http://www.google.com"
            Dim bigrealfile As String = "http://www.mono-project.com/"
            Dim imaginaryFile As String = "http://www.changemeifiexist.nowhere/nofile"

            Dim destination As String = Path.Combine(BASEDIR, "destination")
            Dim localsmallrealfile As String = Path.Combine(BASEDIR, "localsmallrealfile")
            Dim localbigrealfile As String = Path.Combine(BASEDIR, "localbigrealfile")

            Dim pwd As String
            Dim user As String
            Dim showUI As Boolean
            Dim timeout As Integer
            Dim onCancel As Microsoft.VisualBasic.FileIO.UICancelOption
            Dim credentials As Net.NetworkCredential
            Dim smalluri, biguri, imaginaryuri As Uri
            Dim i As Integer
            Dim overwrite As Boolean

            Using client As New Net.WebClient()
                client.DownloadFile(smallrealFile, localsmallrealfile)
                client.DownloadFile(bigrealfile, localbigrealfile)
            End Using

            smalluri = New Uri(smallrealFile)
            biguri = New Uri(bigrealfile)
            imaginaryuri = New Uri(imaginaryFile)


            '-----------------------------
            i += 1
            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-01")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination)
            Helper.CompareFile(destination, localbigrealfile, testname & "-02")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination)
                Assert.Fail(testname & "-03 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-04")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, pwd, user)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = "pwd"
            user = "user"
            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, pwd, user)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user)
                Assert.Fail(testname & "-" & i & "03 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = False

            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True

            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True

            nw.DownloadFile(smalluri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try



            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing

            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            Try
                nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "1" & "?")
            Catch ex As OperationCanceledException
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "2")
            End Try

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = True
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try


            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = False
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException
            credentials = New Net.NetworkCredential(user, pwd)

            IO.File.Delete(destination)
            nw.DownloadFile(smalluri, destination, credentials, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localsmallrealfile, testname & "-" & i & "1")

            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try
            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = False
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException
            credentials = New Net.NetworkCredential(user, pwd)

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            Try
                nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "1" & "?")
            Catch ex As OperationCanceledException
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "2")
            End Try

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

            '-----------------------------
            i += 1
            pwd = String.Empty
            user = String.Empty
            showUI = True
            timeout = 100000
            overwrite = False
            onCancel = Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing
            credentials = New Net.NetworkCredential(user, pwd)

            MsgBox("Please cancel the next download.")
            IO.File.Delete(destination)
            nw.DownloadFile(biguri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
            Helper.CompareFile(destination, localbigrealfile, testname & "-" & i & "2")

            Try
                IO.File.Delete(destination)
                nw.DownloadFile(imaginaryuri, destination, pwd, user, showUI, timeout, overwrite, onCancel)
                Assert.Fail(testname & "-" & i & "3 " & "?")
            Catch ex As Exception
                Assert.AreEqual("?", ex.Message, testname & "-" & i & "4")
            End Try

        End Sub

        <Category("Slow")> _
        <Test()> _
        <Category("UI")> _
        Public Sub UploadFileTest1()
            Dim testname As String = "UploadFileTest1"
            Dim nw As New Network()

            If Not ConnectedToInternet() Then
                Assert.Ignore("No internet connection, so skipping upload tests")
                Return
            End If

            Assert.Ignore("Need some server to where to upload for this to work")

        End Sub

        Private Sub MsgBox(ByVal Message As String, Optional ByVal Style As Microsoft.VisualBasic.MsgBoxStyle = Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Information)
            Microsoft.VisualBasic.Interaction.MsgBox(Message, Style Or Microsoft.VisualBasic.MsgBoxStyle.SystemModal)
        End Sub
    End Class
End Namespace
#End If
