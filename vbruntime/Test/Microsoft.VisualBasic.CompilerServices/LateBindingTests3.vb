'
' LateBindingTests3.vb
'
' Author:
'   Boris Kirzner (borisk@mainsoft.com)
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

Imports System
Imports System.Reflection
Imports NUnit.Framework

<TestFixture()> _
Public Class LateBindingTests3

    Private Class A
    End Class

    Private Class B
        Inherits A
    End Class

    Private Class C
        Inherits B
    End Class

    Private Class D
        Inherits C
    End Class

    Dim a1 As A = New A
    Dim b1 As B = New B
    Dim c1 As C = New C
    Dim d1 As D = New D

    Private Class C400
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class



    <Test()> _
    Public Sub LateBind_ComplexTypes_100()
        Dim o As Object = New C400

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    Private Class C410
        'Public Function F(ByVal a As A) As String
        '    Return "A"
        'End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_110()
        Dim o As Object = New C410

        'Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_ComplexTypes_111()
        Dim o As Object = New C410

        o.F(a1)

    End Sub

    Private Class C420
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        'Public Function F(ByVal a As B) As String
        '    Return "B"
        'End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_120()
        Dim o As Object = New C420

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("A", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    Private Class C430
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        'Public Function F(ByVal a As C) As String
        '    Return "C"
        'End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_130()
        Dim o As Object = New C430

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("B", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    Private Class C440
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        'Public Function F(ByVal a As D) As String
        '    Return "D"
        'End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_140()
        Dim o As Object = New C440

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("C", o.F(d1))
    End Sub

    Private Class C450
        'Public Function F(ByVal a As A) As String
        '    Return "A"
        'End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        'Public Function F(ByVal a As C) As String
        '    Return "C"
        'End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_150()
        Dim o As Object = New C450

        'Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("B", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_ComplexTypes_151()
        Dim o As Object = New C450

        o.F(a1)

    End Sub

    Private Class C460
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        'Public Function F(ByVal a As B) As String
        '    Return "B"
        'End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        'Public Function F(ByVal a As D) As String
        '    Return "D"
        'End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_160()
        Dim o As Object = New C460

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("A", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("C", o.F(d1))
    End Sub

    Private Class C470
        'Public Function F(ByVal a As A) As String
        '    Return "A"
        'End Function

        'Public Function F(ByVal a As B) As String
        '    Return "B"
        'End Function

        Public Function F(ByVal a As C) As String
            Return "C"
        End Function

        Public Function F(ByVal a As D) As String
            Return "D"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_170()
        Dim o As Object = New C470

        'Assert.AreEqual("A", o.F(a1))
        'Assert.AreEqual("A", o.F(b1))
        Assert.AreEqual("C", o.F(c1))
        Assert.AreEqual("D", o.F(d1))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_ComplexTypes_171()
        Dim o As Object = New C470

        o.F(a1)

    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_ComplexTypes_172()
        Dim o As Object = New C470

        o.F(b1)

    End Sub

    Private Class C480
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a As B) As String
            Return "B"
        End Function

        'Public Function F(ByVal a As C) As String
        '    Return "C"
        'End Function

        'Public Function F(ByVal a As D) As String
        '    Return "D"
        'End Function
    End Class

    <Test()> _
    Public Sub LateBind_ComplexTypes_180()
        Dim o As Object = New C480

        Assert.AreEqual("A", o.F(a1))
        Assert.AreEqual("B", o.F(b1))
        Assert.AreEqual("B", o.F(c1))
        Assert.AreEqual("B", o.F(d1))
    End Sub

End Class