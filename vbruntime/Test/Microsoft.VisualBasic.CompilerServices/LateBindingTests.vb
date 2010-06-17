'
' LateBindingTests.vb
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
Public Class LateBindingTests

    Dim bo As Boolean = True
    Dim b As Byte = 1
    Dim s As Short = 1
    Dim i As Integer = 1
    Dim l As Long = 1
    Dim c As Char = "a"c
    Dim d As Double = 1.0
    Dim si As Single = 1.0
    Dim sb As SByte = 1
    Dim us As UShort = 1
    Dim ui As UInteger = 1
    Dim ul As ULong = 1

    <TestFixture()> _
    Public Class Bug344217
        Public Class StrangeClass
            Sub TheFunc(ByRef output(,) As Double)
                output(0, 0) = 1.0
            End Sub
        End Class

        <Test()> _
       Public Sub Main()

            Dim o As Object
            o = New StrangeClass
            Dim data(1, 1) As Double
            o.TheFunc(data)
            Assert.AreEqual(1.0, data(0, 0), "0,0")
            Assert.AreEqual(0.0, data(0, 1), "0,1")
            Assert.AreEqual(0.0, data(1, 0), "1,0")
            Assert.AreEqual(0.0, data(1, 1), "1,1")
        End Sub
    End Class

    <Test()> _
    Public Sub A_ShouldFailWithMono()
        ' FIXME: used to determine the environment the tests run in. Should be removed.
        Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()
    End Sub

    Private Class C1
        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_1()
        Dim o As Object = New C1

        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
    End Sub

    Private Class C2
        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_11()
        Dim o As Object = New C2

        Assert.AreEqual("Short", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Long", o.F(i))
        Assert.AreEqual("Long", o.F(l))
    End Sub

    Private Class C3
        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_12()
        Dim o As Object = New C3

        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Integer", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_2()
        Dim o As Object = New C3

        o.F(l)
    End Sub

    Private Class C4
        Public Function F(ByVal s1 As Byte, ByVal s2 As Byte) As String
            Return "Byte,Byte"
        End Function

        Public Function F(ByVal s1 As Short, ByVal s2 As Short) As String
            Return "Short,Short"
        End Function

        Public Function F(ByVal s1 As Integer, ByVal s2 As Integer) As String
            Return "Integer,Integer"
        End Function

        Public Function F(ByVal s1 As Long, ByVal s2 As Long) As String
            Return "Long,Long"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_21()
        Dim o As Object = New C4

        Assert.AreEqual("Byte,Byte", o.F(b, b))
        Assert.AreEqual("Short,Short", o.F(s, s))
        Assert.AreEqual("Integer,Integer", o.F(i, i))
        Assert.AreEqual("Long,Long", o.F(l, l))
    End Sub

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_22()
        Dim o As Object = New C4

        Assert.AreEqual("Short,Short", o.F(b, s))
        Assert.AreEqual("Short,Short", o.F(s, b))

        Assert.AreEqual("Integer,Integer", o.F(b, i))
        Assert.AreEqual("Integer,Integer", o.F(i, b))
        Assert.AreEqual("Integer,Integer", o.F(i, s))
        Assert.AreEqual("Integer,Integer", o.F(s, i))

        Assert.AreEqual("Long,Long", o.F(b, l))
        Assert.AreEqual("Long,Long", o.F(l, b))
        Assert.AreEqual("Long,Long", o.F(s, l))
        Assert.AreEqual("Long,Long", o.F(l, s))
        Assert.AreEqual("Long,Long", o.F(i, l))
        Assert.AreEqual("Long,Long", o.F(l, i))
    End Sub

    Private Class C5

        Public Function F(ByVal s1 As Short, ByVal s2 As Short) As String
            Return "Short,Short"
        End Function

        Public Function F(ByVal s1 As Long, ByVal s2 As Long) As String
            Return "Long,Long"
        End Function
    End Class

    <Test()> _
   Public Sub LateBind_PrimitiveTypes_3()
        Dim o As Object = New C5

        Assert.AreEqual("Short,Short", o.F(b, s))
        Assert.AreEqual("Short,Short", o.F(s, b))

        Assert.AreEqual("Long,Long", o.F(b, i))
        Assert.AreEqual("Long,Long", o.F(i, b))
        Assert.AreEqual("Long,Long", o.F(i, s))
        Assert.AreEqual("Long,Long", o.F(s, i))

        Assert.AreEqual("Long,Long", o.F(b, l))
        Assert.AreEqual("Long,Long", o.F(l, b))
        Assert.AreEqual("Long,Long", o.F(s, l))
        Assert.AreEqual("Long,Long", o.F(l, s))
        Assert.AreEqual("Long,Long", o.F(i, l))
        Assert.AreEqual("Long,Long", o.F(l, i))
    End Sub

    Private Class C6
        Public Function F(ByVal s1 As Byte, ByVal s2 As Byte) As String
            Return "Byte,Byte"
        End Function

        Public Function F(ByVal s1 As Integer, ByVal s2 As Integer) As String
            Return "Integer,Integer"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_31()
        Dim o As Object = New C6

        Assert.AreEqual("Integer,Integer", o.F(b, s))
        Assert.AreEqual("Integer,Integer", o.F(s, b))

        Assert.AreEqual("Integer,Integer", o.F(b, i))
        Assert.AreEqual("Integer,Integer", o.F(i, b))
        Assert.AreEqual("Integer,Integer", o.F(i, s))
        Assert.AreEqual("Integer,Integer", o.F(s, i))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_4()
        Dim o As Object = New C6

        o.F(b, l)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_5()
        Dim o As Object = New C6

        o.F(l, b)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_6()
        Dim o As Object = New C6

        o.F(s, l)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_7()
        Dim o As Object = New C6

        o.F(l, s)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_8()
        Dim o As Object = New C6

        o.F(i, l)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_9()
        Dim o As Object = New C6

        o.F(l, i)
    End Sub

    Private Class C100

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_100()
        Dim o As Object = New C100

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C200

        'Public Function F(ByVal s As Boolean) As String
        '    Return "Boolean"
        'End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_200()
        Dim o As Object = New C200

        'Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_201()
        Dim o As Object = New C200

        o.F(bo)

    End Sub

    Private Class C210

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        'Public Function F(ByVal s As Byte) As String
        '    Return "Byte"
        'End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_210()
        Dim o As Object = New C210

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Short", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C220

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        'Public Function F(ByVal s As SByte) As String
        '    Return "SByte"
        'End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class
    'TargetJvmNotWorking - Assert.AreEqual("Short", o.F(sb)) Fail - support for 2.0 sbyte
    <Category("TargetJvmNotWorking"), Test()> _
    Public Sub LateBind_PrimitiveTypes_220()
        Dim o As Object = New C220

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("Short", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C230

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        'Public Function F(ByVal s As UShort) As String
        '    Return "UShort"
        'End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_230()
        Dim o As Object = New C230

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("Integer", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C240

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        'Public Function F(ByVal s As UInteger) As String
        '    Return "UInteger"
        'End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_240()
        Dim o As Object = New C240

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("Long", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C250

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        'Public Function F(ByVal s As ULong) As String
        '    Return "ULong"
        'End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_250()
        Dim o As Object = New C250

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("Single", o.F(ul))
    End Sub

    Private Class C260

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        'Public Function F(ByVal s As Integer) As String
        '    Return "Integer"
        'End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Category("TargetJvmNotWorking"), Test()> _
    Public Sub LateBind_PrimitiveTypes_260()
        Dim o As Object = New C260

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Long", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C270

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        'Public Function F(ByVal s As Long) As String
        '    Return "Long"
        'End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Category("TargetJvmNotWorking"), Test()> _
    Public Sub LateBind_PrimitiveTypes_270()
        Dim o As Object = New C270

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Single", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C280

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        'Public Function F(ByVal s As Char) As String
        '    Return "Char"
        'End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_280()
        Dim o As Object = New C280

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        'Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_281()
        Dim o As Object = New C280

        Dim c As Char = "a"c

        o.F(c)

    End Sub

    Private Class C290

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        'Public Function F(ByVal s As Double) As String
        '    Return "Double"
        'End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_290()
        Dim o As Object = New C290

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        'Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_291()
        Dim o As Object = New C290

        o.F(d)

    End Sub

    Private Class C300

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        'Public Function F(ByVal s As Single) As String
        '    Return "Single"
        'End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_300()
        Dim o As Object = New C300

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Double", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    Private Class C310

        'Public Function F(ByVal s As Boolean) As String
        '    Return "Boolean"
        'End Function

        Public Function F(ByVal s As Byte) As String
            Return "Byte"
        End Function

        'Public Function F(ByVal s As Short) As String
        '    Return "Short"
        'End Function

        Public Function F(ByVal s As SByte) As String
            Return "SByte"
        End Function

        'Public Function F(ByVal s As UShort) As String
        '    Return "UShort"
        'End Function

        Public Function F(ByVal s As UInteger) As String
            Return "UInteger"
        End Function

        'Public Function F(ByVal s As ULong) As String
        '    Return "ULong"
        'End Function

        Public Function F(ByVal s As Integer) As String
            Return "Integer"
        End Function

        'Public Function F(ByVal s As Long) As String
        '    Return "Long"
        'End Function

        Public Function F(ByVal s As Char) As String
            Return "Char"
        End Function

        'Public Function F(ByVal s As Double) As String
        '    Return "Double"
        'End Function

        Public Function F(ByVal s As Single) As String
            Return "Single"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_310()
        Dim o As Object = New C310

        'Assert.AreEqual("Short", o.F(bo))
        Assert.AreEqual("Byte", o.F(b))
        Assert.AreEqual("Integer", o.F(s))
        Assert.AreEqual("Integer", o.F(i))
        Assert.AreEqual("Single", o.F(l))
        Assert.AreEqual("Char", o.F(c))
        'Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Single", o.F(si))
        Assert.AreEqual("SByte", o.F(sb))
        Assert.AreEqual("Integer", o.F(us))
        Assert.AreEqual("UInteger", o.F(ui))
        Assert.AreEqual("Single", o.F(ul))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_311()
        Dim o As Object = New C310

        o.F(d)

    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_PrimitiveTypes_312()
        Dim o As Object = New C310

        o.F(bo)

    End Sub

    Private Class C320

        Public Function F(ByVal s As Boolean) As String
            Return "Boolean"
        End Function

        'Public Function F(ByVal s As Byte) As String
        '    Return "Byte"
        'End Function

        Public Function F(ByVal s As Short) As String
            Return "Short"
        End Function

        'Public Function F(ByVal s As SByte) As String
        '    Return "SByte"
        'End Function

        Public Function F(ByVal s As UShort) As String
            Return "UShort"
        End Function

        'Public Function F(ByVal s As UInteger) As String
        '    Return "UInteger"
        'End Function

        Public Function F(ByVal s As ULong) As String
            Return "ULong"
        End Function

        'Public Function F(ByVal s As Integer) As String
        '    Return "Integer"
        'End Function

        Public Function F(ByVal s As Long) As String
            Return "Long"
        End Function

        'Public Function F(ByVal s As Char) As String
        '    Return "Char"
        'End Function

        Public Function F(ByVal s As Double) As String
            Return "Double"
        End Function

        'Public Function F(ByVal s As Single) As String
        '    Return "Single"
        'End Function
    End Class

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_320()
        Dim o As Object = New C320

        Assert.AreEqual("Boolean", o.F(bo))
        Assert.AreEqual("Short", o.F(b))
        Assert.AreEqual("Short", o.F(s))
        Assert.AreEqual("Long", o.F(i))
        Assert.AreEqual("Long", o.F(l))
        'Assert.AreEqual("Char", o.F(c))
        Assert.AreEqual("Double", o.F(d))
        Assert.AreEqual("Double", o.F(si))
        Assert.AreEqual("Short", o.F(sb))
        Assert.AreEqual("UShort", o.F(us))
        Assert.AreEqual("Long", o.F(ui))
        Assert.AreEqual("ULong", o.F(ul))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
  Public Sub LateBind_PrimitiveTypes_321()
        Dim o As Object = New C320

        o.F(c)

    End Sub

    Private Class C400
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function
    End Class

    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Public Sub LateBind_PrimitiveTypes_400()
        Dim o As Object = New C400

        o.F("w")

    End Sub

    <Test()> _
    Public Sub LateBind_PrimitiveTypes_402()
        Dim o As Object = New C400

        Assert.AreEqual("Integer", o.F("23"))

    End Sub

    Private Class C401
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal b As Boolean)
            Return "Boolean"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_401()
        Dim o As Object = New C401

        o.F("True")
    End Sub

    Private Class C402
        Public Function F(ByVal b As Boolean)
            Return "Boolean"
        End Function

        Public Function F(ByVal b As Short)
            Return "Short"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_403()
        Dim o As Object = New C402

        o.F(i)

    End Sub

    Private Class C403
        Public Function F(ByVal c As Integer)
            Return "Integer"
        End Function
    End Class

    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Public Sub LateBind_PrimitiveTypes_404()
        Dim o As Object = New C403

        o.F(c)

    End Sub

    Private Class C404
        Public Function F(ByVal c As Short)
            Return "Short"
        End Function

        Public Function F(ByVal c As String)
            Return "String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_PrimitiveTypes_405()
        Dim o As Object = New C404

        o.F(i)

    End Sub

    Private Class C500
        Public Function F(ByVal s As Short, ByVal i As Integer) As String
            Return "Short,Integer"
        End Function

        Public Function F(ByVal l As Long, ByVal s As String) As String
            Return "Long,String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_PrimitiveTypes_500()
        Dim o As Object = New C500

        o.F(i, i)

    End Sub

End Class