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
Public Class VBFixedArrayAttributeTest

    <Test()> _
    Public Sub TestCtor()
        Dim v As VBFixedArrayAttribute

        ' first ctor

        Try
            v = New VBFixedArrayAttribute(-1)
            Assert.Fail("Expected ArgumentException - #-01")
        Catch ex As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedArrayAttribute' are not valid.", ex.Message, "#-01 msg")
        End Try

        v = New VBFixedArrayAttribute(0)
        Assert.AreEqual(1, v.Length, "#02 Length")
        Assert.AreEqual(1, v.Bounds.Length, "#02 Bounds.Length")
        Assert.AreEqual(0, v.Bounds(0), "#02 Bounds (0)")

        v = New VBFixedArrayAttribute(1)
        Assert.AreEqual(2, v.Length, "#03 Length")
        Assert.AreEqual(1, v.Bounds.Length, "#03 Bounds.Length")
        Assert.AreEqual(1, v.Bounds(0), "#03 Bounds (0)")

        v = New VBFixedArrayAttribute(2)
        Assert.AreEqual(3, v.Length, "#04 Length")
        Assert.AreEqual(1, v.Bounds.Length, "#04 Bounds.Length")
        Assert.AreEqual(2, v.Bounds(0), "#04 Bounds (0)")

        v = New VBFixedArrayAttribute(Integer.MaxValue)
        Try
            Assert.AreEqual(Integer.MaxValue, v.Length, "#05 Length")
            Assert.Fail("Expected OverflowException - #05 Length overflow")
        Catch ex As OverflowException
            Assert.AreEqual("Arithmetic operation resulted in an overflow.", ex.Message, "#05 Length ex")
        End Try
        Assert.AreEqual(1, v.Bounds.Length, "#05 Bounds.Length")
        Assert.AreEqual(Integer.MaxValue, v.Bounds(0), "#05 Bounds (0)")

        v = New VBFixedArrayAttribute(Integer.MaxValue - 1)
        Assert.AreEqual(Integer.MaxValue, v.Length, "#06 Length")
        Assert.AreEqual(1, v.Bounds.Length, "#06 Bounds.Length")
        Assert.AreEqual(Integer.MaxValue - 1, v.Bounds(0), "#06 Bounds (0)")

        ' second ctor

        v = New VBFixedArrayAttribute(0, 0)
        Assert.AreEqual(1, v.Length, "#10 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#10 Bounds.Length")
        Assert.AreEqual(0, v.Bounds(0), "#10 Bounds (0)")
        Assert.AreEqual(0, v.Bounds(1), "#10 Bounds (1)")

        Try
            v = New VBFixedArrayAttribute(-1, 0)
            Assert.Fail("Expected ArgumentException - #11")
        Catch ex As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedArrayAttribute' are not valid.", ex.Message, "#-11 msg")
        End Try

        Try
            v = New VBFixedArrayAttribute(0, -1)
            Assert.Fail("Expected ArgumentException - #12")
        Catch ex As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedArrayAttribute' are not valid.", ex.Message, "#-12 msg")
        End Try

        Try
            v = New VBFixedArrayAttribute(-1, -1)
            Assert.Fail("Expected ArgumentException - #13")
        Catch ex As ArgumentException
            Assert.AreEqual("Arguments to 'VBFixedArrayAttribute' are not valid.", ex.Message, "#-13 msg")
        End Try

        v = New VBFixedArrayAttribute(1, 0)
        Assert.AreEqual(2, v.Length, "#14 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#14 Bounds.Length")
        Assert.AreEqual(1, v.Bounds(0), "#14 Bounds (0)")
        Assert.AreEqual(0, v.Bounds(1), "#14 Bounds (1)")


        v = New VBFixedArrayAttribute(1, 1)
        Assert.AreEqual(4, v.Length, "#15 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#15 Bounds.Length")
        Assert.AreEqual(1, v.Bounds(0), "#15 Bounds (0)")
        Assert.AreEqual(1, v.Bounds(1), "#15 Bounds (1)")

        v = New VBFixedArrayAttribute(1, 3)
        Assert.AreEqual(8, v.Length, "#16 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#16 Bounds.Length")
        Assert.AreEqual(1, v.Bounds(0), "#16 Bounds (0)")
        Assert.AreEqual(3, v.Bounds(1), "#16 Bounds (1)")

        v = New VBFixedArrayAttribute(7, 3)
        Assert.AreEqual(32, v.Length, "#17 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#17 Bounds.Length")
        Assert.AreEqual(7, v.Bounds(0), "#17 Bounds (0)")
        Assert.AreEqual(3, v.Bounds(1), "#17 Bounds (1)")

        v = New VBFixedArrayAttribute(Short.MaxValue, Short.MaxValue)
        Assert.AreEqual((Short.MaxValue + 1) * (Short.MaxValue + 1), v.Length, "#18 Length")
        Assert.AreEqual(2, v.Bounds.Length, "#18 Bounds.Length")
        Assert.AreEqual(Short.MaxValue, v.Bounds(0), "#18 Bounds (0)")
        Assert.AreEqual(Short.MaxValue, v.Bounds(1), "#18 Bounds (1)")

        v = New VBFixedArrayAttribute(Integer.MaxValue - 1, Integer.MaxValue - 1)
        Try
            Assert.AreEqual(Integer.MaxValue, v.Length, "#19 Length")
            Assert.Fail("Expected OverflowException - #19 Length overflow")
        Catch ex As OverflowException
            Assert.AreEqual("Arithmetic operation resulted in an overflow.", ex.Message, "#19 Length ex")
        End Try
        Assert.AreEqual(2, v.Bounds.Length, "#19 Bounds.Length")
        Assert.AreEqual(Integer.MaxValue - 1, v.Bounds(0), "#19 Bounds (0)")
        Assert.AreEqual(Integer.MaxValue - 1, v.Bounds(1), "#19 Bounds (1)")

    End Sub
End Class
