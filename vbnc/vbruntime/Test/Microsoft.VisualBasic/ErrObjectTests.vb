' ErrObjectTests.vb - NUnit Test Cases for Microsoft.VisualBasic.ErrObject 
'
' Mizrahi Rafael (rafim@mainsoft.com)
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
Imports System.Collections
Imports Microsoft.VisualBasic
<TestFixture()> _
Public Class ErrObjectTests
    <SetUp()> _
    Public Sub GetReady()
    End Sub

    <TearDown()> _
    Public Sub Clean()
    End Sub

#Region "On Error tests"
    <Test()> _
    Public Sub OnError1()
        On Error Resume Next
        Dim i As Integer
        Dim j As Integer

        i = 10
        j = 0
        i = i / j
        Assert.AreEqual(True, True)
    End Sub


#End Region


#Region "Err.Number tests"

    <Test()> _
    Public Sub ErrNumber1()
        Dim i As Integer
        Dim j As Integer

        Try
            i = 10
            j = 0
            i = i / j
        Catch ex As Exception
            Assert.AreEqual(6, Err.Number)
        End Try
    End Sub
    <Test()> _
    Public Sub ErrNumber2()

        'Dim i As Integer
        Dim caughtException As Boolean

        ' Number is greater than 65535
        '
        caughtException = False
        Try
            Err.Raise(65536)
        Catch e As ArgumentException
            Assert.AreEqual(5, Err.Number)
            caughtException = True
        End Try
        Assert.AreEqual(True, caughtException)

        ' Number is greater than 513
        '
        caughtException = False
        Try
            Err.Raise(514)
        Catch e As Exception
            Assert.AreEqual(514, Err.Number)
            caughtException = True
        End Try
        Assert.AreEqual(True, caughtException)
    End Sub

    <Test()> _
    Public Sub ErrNumber3()
        Try
            Err.Raise(vbObjectError - 1) ' vbObjectError=-2147221504
        Catch e As Exception
            Assert.AreEqual(vbObjectError - 1, Err.Number)
        End Try
    End Sub
#End Region

#Region "Raise tests"

    <Test(), ExpectedException(GetType(Exception))> _
    Public Sub Raise1()
        Dim err As ErrObject
        err = Information.Err()
        err.Raise(1, "source", "description", "", 0)
    End Sub
    <Test(), ExpectedException(GetType(Exception))> _
    Public Sub Raise2()
        Dim err As ErrObject
        err = Information.Err()
        err.Raise(2, "source", "description", "", 0)
    End Sub
    <Test()> _
    Public Sub Raise3()
        Dim i As Integer
        Dim caughtException As Boolean
        For i = 1 To 600 ' more then 513 and less than 65535
            caughtException = False
            Try
                Err.Raise(i)
            Catch ex As Exception
                If Err.Number = i Then
                    caughtException = True
                End If
            End Try
            Assert.AreEqual(True, caughtException, "failed at sub test " & i)
            'If caughtException = False Then Return "failed at sub test " & i
        Next i
    End Sub
#End Region


End Class

