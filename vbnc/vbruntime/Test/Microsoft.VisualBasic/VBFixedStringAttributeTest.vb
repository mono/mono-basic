'
' VBFixedStringAttributeTest.vb
'
' Author:
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' Copyright (C) 2009 Novell
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
Public Class VBFixedStringAttributeTest
    <Test()> _
    Public Sub TestCtor()
        Dim v As VBFixedStringAttribute

        Try
            v = New VBFixedStringAttribute(-1)
            Assert.Fail("Expected ArgumentException - #-1")
        Catch e As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedStringAttribute' are not valid.", e.Message, "#-1-msg")
        End Try

        Try
            v = New VBFixedStringAttribute(0)
            Assert.Fail("Expected ArgumentException - #01")
        Catch e As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedStringAttribute' are not valid.", e.Message, "#01-msg")
        End Try

        v = New VBFixedStringAttribute(1)
        Assert.AreEqual(1, v.Length, "#02")

        v = New VBFixedStringAttribute(5)
        Assert.AreEqual(5, v.Length, "#03")

        v = New VBFixedStringAttribute(32767)
        Assert.AreEqual(32767, v.Length, "#04")

        Try
            v = New VBFixedStringAttribute(32768)
            Assert.Fail("Expected ArgumentException - #05")
        Catch e As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedStringAttribute' are not valid.", e.Message, "#05-msg")
        End Try

    End Sub
End Class
