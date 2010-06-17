'
' StringsTest.vb
'
' Author:
'   Kornél Pál <http://www.kornelpal.hu/>
'
' Copyright (C) 2008 Kornél Pál
'

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

Option Strict On

Imports System
Imports System.Globalization
Imports System.Threading
Imports System.Text
Imports Microsoft.VisualBasic
Imports NUnit.Framework

<TestFixture()> _
Public Class StringsTest
    Public Sub New()
    End Sub

    <Test()> _
    Public Sub TestAscW()
        Dim i As Integer

        For i = 0 To 65535
            Assert.AreEqual(i, AscW(Convert.ToChar(i)))
        Next
    End Sub

    <Test()> _
    Public Sub TestChrW()
        Dim i As Integer
        Dim ch As Char

        For i = 0 To 32767
            Assert.AreEqual(Convert.ToChar(i), ChrW(i))
        Next
        For i = 32768 To 65535
            ch = Convert.ToChar(i)
            Assert.AreEqual(ch, ChrW(i))
            Assert.AreEqual(ch, ChrW(i - 65536))
        Next
    End Sub

    <Test()> _
    Public Sub TestAscInstalledCulture()
        TestAscWorker(CultureInfo.InstalledUICulture)
    End Sub

    <Test()> _
    Public Sub TestAscEnUs()
        TestAscWorker(New CultureInfo(&H409, False))
    End Sub

    <Test()> _
    Public Sub TestAscJaJp()
        TestAscWorker(New CultureInfo(&H411, False))
    End Sub

    <Test()> _
    Public Sub TestAscZhCn()
        TestAscWorker(New CultureInfo(&H804, False))
    End Sub

    Public Sub TestAscWorker(ByVal culture As CultureInfo)
        Dim currentCulture As CultureInfo = Thread.CurrentThread.CurrentCulture

        Thread.CurrentThread.CurrentCulture = culture
        Try
            Me.TestAscWorker()
        Finally
            Thread.CurrentThread.CurrentCulture = currentCulture
        End Try
    End Sub

    Public Sub TestAscWorker()
        Dim i As Integer
        Dim enc As Encoding = Encoding.Default
        Dim chars As Char() = New Char(0) {}
        Dim bytes As Byte() = New Byte(1) {}
        Dim byteCount As Integer
        Dim expectEx As Boolean
        Dim expectCharCode As Integer
        Dim charCode As Integer

        For i = 0 To 127
            Assert.AreEqual(i, Asc(ChrW(i)))
        Next

        For i = 128 To 65535
            chars(0) = ChrW(i)

            expectEx = False
            Try
                byteCount = enc.GetBytes(chars, 0, 1, bytes, 0)
            Catch e As ArgumentException
                expectEx = True
            End Try

            If Not expectEx Then
                Select Case byteCount
                    Case 0
                        expectCharCode = 0
                    Case 1
                        expectCharCode = bytes(0)
                    Case 2
                        expectCharCode = (CInt(bytes(0)) << 8) Or bytes(1)
                    Case Else
                        Assert.Fail("Invalid byte count.")
                End Select
            End If

            Try
                charCode = Asc(ChrW(i))
                If expectEx Then
                    Assert.Fail("ArgumentException was expected.")
                End If
            Catch e As ArgumentException
                If Not expectEx Then
                    Assert.Fail("ArgumentException was not expected.")
                End If
            End Try

            Assert.AreEqual(expectCharCode, charCode)
        Next
    End Sub

    <Test()> _
    Public Sub TestChrInstalledCulture()
        TestChrWorker(CultureInfo.InstalledUICulture)
    End Sub

    <Test()> _
    Public Sub TestChrEnUs()
        TestChrWorker(New CultureInfo(&H409, False))
    End Sub

    <Test()> _
    Public Sub TestChrJaJp()
        TestChrWorker(New CultureInfo(&H411, False))
    End Sub

    <Test()> _
    Public Sub TestChrZhCn()
        TestChrWorker(New CultureInfo(&H804, False))
    End Sub

    Public Sub TestChrWorker(ByVal culture As CultureInfo)
        Dim currentCulture As CultureInfo = Thread.CurrentThread.CurrentCulture

        Thread.CurrentThread.CurrentCulture = culture
        Try
            Me.TestChrWorker()
        Finally
            Thread.CurrentThread.CurrentCulture = currentCulture
        End Try
    End Sub

    Public Sub TestChrWorker()
        Dim i As Integer
        Dim enc As Encoding = Encoding.GetEncoding(Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage)
        Dim decoder As Decoder
        Dim bytes As Byte() = New Byte(1) {}
        Dim chars As Char() = New Char(1) {}
        Dim charCount As Integer
        Dim expectEx As Boolean
        Dim expectCh As Integer
        Dim ch As Integer

        For i = 0 To 127
            Assert.AreEqual(ChrW(i), Chr(i))
        Next

        For i = 128 To 255
            bytes(0) = CByte(i)
            decoder = enc.GetDecoder()

            expectEx = False
            Try
                charCount = decoder.GetChars(bytes, 0, 1, chars, 0)
            Catch e As ArgumentException
                expectEx = True
            End Try

            If Not expectEx Then
                If charCount = 0 Then
                    expectCh = 0
                Else
                    expectCh = AscW(chars(0))
                End If
            End If

            Try
                ch = AscW(Chr(i))
                If expectEx Then
                    Assert.Fail("ArgumentException was expected.")
                End If
            Catch e As ArgumentException
                If Not expectEx Then
                    Assert.Fail("ArgumentException was not expected.")
                End If
            End Try

            Assert.AreEqual(expectCh, ch)
        Next

        If enc.IsSingleByte Then
            For i = 256 To 32767
                Try
                    ch = AscW(Chr(i))
                    Assert.Fail("ArgumentException was expected.")
                Catch e As ArgumentException
                End Try
            Next

            For i = 32768 To 65535
                Try
                    ch = AscW(Chr(i))
                    Assert.Fail("ArgumentException was expected.")
                Catch e As ArgumentException
                End Try

                Try
                    ch = AscW(Chr(i - 65536))
                    Assert.Fail("ArgumentException was expected.")
                Catch e As ArgumentException
                End Try
            Next
        Else
            For i = 256 To 32767
                bytes(0) = CByte((i And &HFF00) >> 8)
                bytes(1) = CByte(i And &HFF)
                decoder = enc.GetDecoder()

                expectEx = False
                Try
                    charCount = decoder.GetChars(bytes, 0, 2, chars, 0)
                Catch e As ArgumentException
                    expectEx = True
                End Try

                If Not expectEx Then
                    If charCount = 0 Then
                        expectCh = 0
                    Else
                        expectCh = AscW(chars(0))
                    End If
                End If

                Try
                    ch = AscW(Chr(i))
                    If expectEx Then
                        Assert.Fail("ArgumentException was expected.")
                    End If
                Catch e As ArgumentException
                    If Not expectEx Then
                        Assert.Fail("ArgumentException was not expected.")
                    End If
                End Try

                Assert.AreEqual(expectCh, ch)
            Next

            For i = 32768 To 65535
                bytes(0) = CByte((i And &HFF00) >> 8)
                bytes(1) = CByte(i And &HFF)
                decoder = enc.GetDecoder()

                expectEx = False
                Try
                    charCount = decoder.GetChars(bytes, 0, 2, chars, 0)
                Catch e As ArgumentException
                    expectEx = True
                End Try

                If Not expectEx Then
                    If charCount = 0 Then
                        expectCh = 0
                    Else
                        expectCh = AscW(chars(0))
                    End If
                End If

                Try
                    ch = AscW(Chr(i))
                    If expectEx Then
                        Assert.Fail("ArgumentException was expected.")
                    End If
                Catch e As ArgumentException
                    If Not expectEx Then
                        Assert.Fail("ArgumentException was not expected.")
                    End If
                End Try

                Assert.AreEqual(expectCh, ch)

                Try
                    ch = AscW(Chr(i - 65536))
                    If expectEx Then
                        Assert.Fail("ArgumentException was expected.")
                    End If
                Catch e As ArgumentException
                    If Not expectEx Then
                        Assert.Fail("ArgumentException was not expected.")
                    End If
                End Try

                Assert.AreEqual(expectCh, ch)
            Next
        End If
    End Sub
End Class
