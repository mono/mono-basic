'
' LateBindingTests2.vb
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
Public Class LateBindingTests2

    Dim bo As Boolean = True
    Dim b As Byte = 1
    Dim s As Short = 1
    Dim i As Integer = 1
    Dim l As Long = 1
    Dim c As Char = "a"c
    Dim d As Double = 1.0
    Dim si As Single = 1.0

    Dim ss2 As String = "aa"
    Dim ccA As A = New A
    Dim ccB As BB = New BB
    Dim ccc As CC = New CC
    Dim iic As IConvertible = New IC

    Dim sb As SByte = 1
    Dim us As UShort = 1
    Dim ui As UInteger = 1
    Dim ul As ULong = 1

    Private Class C1
        Public Function F(ByVal o As Object) As String
            Return "Object"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Object()
        Dim o As Object = New C1

        Assert.AreEqual("Object", o.F(bo))
        Assert.AreEqual("Object", o.F(b))
        Assert.AreEqual("Object", o.F(s))
        Assert.AreEqual("Object", o.F(i))
        Assert.AreEqual("Object", o.F(l))
        Assert.AreEqual("Object", o.F(c))
        Assert.AreEqual("Object", o.F(d))
        Assert.AreEqual("Object", o.F(si))

        Assert.AreEqual("Object", o.F(ccA))
        Assert.AreEqual("Object", o.F(ss2))
        Assert.AreEqual("Object", o.F(iic))

        Assert.AreEqual("Object", o.F(sb))
        Assert.AreEqual("Object", o.F(us))
        Assert.AreEqual("Object", o.F(ui))
        Assert.AreEqual("Object", o.F(ul))
    End Sub

    Private Class C2
        Public Function F(ByVal o As String) As String
            Return "String"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_String()
        Dim o As Object = New C2

        Assert.AreEqual("String", o.F(bo))
        Assert.AreEqual("String", o.F(b))
        Assert.AreEqual("String", o.F(s))
        Assert.AreEqual("String", o.F(i))
        Assert.AreEqual("String", o.F(l))
        Assert.AreEqual("String", o.F(c))
        Assert.AreEqual("String", o.F(d))
        Assert.AreEqual("String", o.F(si))

        'Assert.AreEqual("String", o.F(ccA))
        Assert.AreEqual("String", o.F(ss2))
        'Assert.AreEqual("Object", o.F(iic))

        Assert.AreEqual("String", o.F(sb))
        Assert.AreEqual("String", o.F(us))
        Assert.AreEqual("String", o.F(ui))
        Assert.AreEqual("String", o.F(ul))
    End Sub


    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Public Sub LateBind_String2()
        Dim o As Object = New C2

        o.F(ccA)
    End Sub

    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Public Sub LateBind_String3()
        Dim o As Object = New C2

        o.F(iic)
    End Sub

    Private Class C100
        Public Function F(ByVal o As Object) As String
            Return "Object"
        End Function

        Public Function F(ByVal o As String) As String
            Return "String"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_StringObject()
        Dim o As Object = New C100

        Dim ccA As A = New A

        Assert.AreEqual("Object", o.F(bo))
        Assert.AreEqual("Object", o.F(b))
        Assert.AreEqual("Object", o.F(s))
        Assert.AreEqual("Object", o.F(i))
        Assert.AreEqual("Object", o.F(l))
        Assert.AreEqual("String", o.F(c))
        Assert.AreEqual("Object", o.F(d))
        Assert.AreEqual("Object", o.F(si))

        Assert.AreEqual("Object", o.F(ccA))
        Assert.AreEqual("String", o.F(ss2))
        Assert.AreEqual("Object", o.F(iic))

        Assert.AreEqual("Object", o.F(sb))
        Assert.AreEqual("Object", o.F(us))
        Assert.AreEqual("Object", o.F(ui))
        Assert.AreEqual("Object", o.F(ul))
    End Sub

    Private Class C4
        Public Function F(ByVal ParamArray args() As A) As String
            Return "ParamArray A()"
        End Function

        Public Function F(ByVal a As A, ByVal ParamArray args() As A) As String
            Return "A,ParamArray A()"
        End Function
    End Class

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_Complex_ParamArray1()
        Dim o As Object = New C4

        o.F(ccA)
    End Sub

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_Complex_ParamArray2()
        Dim o As Object = New C4

        o.F(ccA, ccA)
    End Sub

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_Complex_ParamArray6()
        Dim o As Object = New C4

        o.F(ccA, ccA, ccA, ccA, ccA, ccA)
    End Sub

    Private Class C5
        Public Function F(ByVal ParamArray args() As A) As String
            Return "ParamArray A()"
        End Function

        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a1 As A, ByVal a2 As A) As String
            Return "A,A"
        End Function

        Public Function F(ByVal a1 As A, ByVal a2 As A, ByVal a3 As A, ByVal a4 As A) As String
            Return "A,A,A,A"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray10()
        Dim o As Object = New C5

        Assert.AreEqual("A", o.F(ccA))
        Assert.AreEqual("A,A", o.F(ccA, ccA))
        Assert.AreEqual("ParamArray A()", o.F(ccA, ccA, ccA))
        Assert.AreEqual("A,A,A,A", o.F(ccA, ccA, ccA, ccA))
        Assert.AreEqual("ParamArray A()", o.F(ccA, ccA, ccA, ccA, ccA))
    End Sub

    <Test()> _
   Public Sub LateBind_Complex_ParamArray11()
        Dim o As Object = New C5

        Assert.AreEqual("A", o.F(ccB))
        Assert.AreEqual("A,A", o.F(ccB, ccB))
        Assert.AreEqual("ParamArray A()", o.F(ccB, ccB, ccB))
        Assert.AreEqual("A,A,A,A", o.F(ccB, ccB, ccB, ccB))
        Assert.AreEqual("ParamArray A()", o.F(ccB, ccB, ccB, ccB, ccB))
    End Sub

    Private Class C6
        Public Function F(ByVal ParamArray args() As A) As String
            Return "ParamArray A()"
        End Function

        Public Function F(ByVal a1 As A, ByVal a2 As A, ByVal a As A) As String
            Return "A,A,A"
        End Function

        Public Function F(ByVal ParamArray args() As BB) As String
            Return "ParamArray BB()"
        End Function

        Public Function F(ByVal a1 As BB, ByVal a2 As BB, ByVal a As BB) As String
            Return "BB,BB,BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray12()
        Dim o As Object = New C6

        Assert.AreEqual("ParamArray A()", o.F(ccA))
        Assert.AreEqual("A,A,A", o.F(ccA, ccA, ccA))
        Assert.AreEqual("ParamArray A()", o.F(ccA, ccA, ccA, ccA, ccA))

        Assert.AreEqual("ParamArray BB()", o.F(ccB))
        Assert.AreEqual("BB,BB,BB", o.F(ccB, ccB, ccB))
        Assert.AreEqual("ParamArray BB()", o.F(ccB, ccB, ccB, ccB, ccB))

        Assert.AreEqual("A,A,A", o.F(ccB, ccA, ccA))
        Assert.AreEqual("ParamArray A()", o.F(ccB, ccA, ccB, ccA, ccB))
    End Sub

    Private Class C7
        Public Function F(ByVal b As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal b As String) As String
            Return "String"
        End Function

        Public Function F(ByVal o As Object) As String
            Return "Object"
        End Function
    End Class

    <Test()> _
     Public Sub LateBind_Complex_IConvertible1()
        Dim o As Object = New C7

        Assert.AreEqual("Object", o.F(iic))
    End Sub

    Private Class C8
        Public Function F(ByVal b As Byte) As String
            Return "Byte"
        End Function

        Public Function F(ByVal b As String) As String
            Return "String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
     Public Sub LateBind_Complex_IConvertible2()
        Dim o As Object = New C8

        o.F(iic)
    End Sub

    Private Class C9
        Public Function F(ByVal b As Integer, ByVal s As String) As String
            Return "Integer,String"
        End Function

        Public Function F(ByVal b As Short, ByVal o As Object) As String
            Return "Short,Object"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Primitive_Object1()
        Dim o As Object = New C9

        o.F(b, c)
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_IConvertible_Primitive1()
        Dim o As Object = New C9
        o.F(iic, c)
    End Sub

    Private Class C10
        Public Function F(ByVal o As Short, ByVal a1 As A)
            Return "Short,A"
        End Function

        Public Function F(ByVal o As Object, ByVal b1 As BB)
            Return "Object,BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_Primitive1()
        Dim o As Object = New C10

        Assert.AreEqual("Short,A", o.F(s, ccA))

    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_Complex_Primitive2()
        Dim o As Object = New C10

        o.F(s, ccB)
    End Sub

    Private Class C11
        Public Function F(ByVal o As Char, ByVal a1 As A)
            Return "Char,A"
        End Function

        Public Function F(ByVal o As String, ByVal b1 As BB)
            Return "Object,BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_Char1()
        Dim o As Object = New C11

        Assert.AreEqual("Char,A", o.F(c, ccA))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Complex_Char2()
        Dim o As Object = New C11

        o.F(c, ccB)
    End Sub

    Private Class C12
        Public Function F(ByVal o As String, ByVal a1 As A)
            Return "String,A"
        End Function

        Public Function F(ByVal o As Object, ByVal b1 As BB)
            Return "Object,BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_Char3()
        Dim o As Object = New C12

        Assert.AreEqual("String,A", o.F(c, ccA))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_Complex_Char4()
        Dim o As Object = New C12

        o.F(c, ccB)
    End Sub

    Private Class C13
        Public Function F(ByVal o As Short, ByVal ParamArray arr() As Integer)
            Return "Short,ParamArray arr() Integer"
        End Function

        Public Function F(ByVal o As Object, ByVal i1 As Integer, ByVal i2 As Integer)
            Return "Object,Integer,Integer"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Primitive_ParamArray1()
        Dim o As Object = New C13

        Assert.AreEqual("Short,ParamArray arr() Integer", o.F(b, i, i))
        Assert.AreEqual("Object,Integer,Integer", o.F(l, i, i))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Primitive_ParamArray2()
        Dim o As Object = New C13

        o.F(b, l, l)
    End Sub

    Private Class C14
        Public Function F(ByVal o1 As Object, ByVal o2 As Object)
            Return "Object,Object"
        End Function

        Public Function F(ByVal o1 As String, ByVal o2 As String)
            Return "String,String"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Primitive_Object2()
        Dim o As Object = New C14

        Assert.AreEqual("Object,Object", o.F(i, c))
    End Sub

    Private Class C15
        Public Function F(ByVal a1 As A, ByVal ParamArray b1() As BB)
            Return "A,ParamArray arr() BB"
        End Function

        Public Function F(ByVal b1 As BB, ByVal b2 As A, ByVal b3 As BB)
            Return "BB,A,BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray20()
        Dim o As Object = New C15

        Assert.AreEqual("BB,A,BB", o.F(ccB, ccA, ccB))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Complex_ParamArray30()
        Dim o As Object = New C15

        o.F(ccB, ccB, ccB)
    End Sub

    Private Class C16
        Public Function F(ByVal b1 As BB, ByVal s As Short)
            Return "BB,Short"
        End Function

        Public Function F(ByVal a As A, ByVal l As Long)
            Return "A,Long"
        End Function
    End Class

    <Test()> _
   Public Sub LateBind_Complex_Primitive10()
        Dim o As Object = New C16

        Assert.AreEqual("A,Long", o.F(ccB, i))
    End Sub

    Private Class C17
        Public Function F(ByVal a As A, ByVal s As Integer)
            Return "A,Integer"
        End Function

        Public Function F(ByVal b1 As BB, ByVal l As Long)
            Return "BB,Long"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Complex_Primitive11()
        Dim o As Object = New C17

        o.F(ccB, i)
    End Sub

    Private Class C18
        Public Function F(ByVal i As Integer, ByVal s As String)
            Return "Integer,Short"
        End Function

        Public Function F(ByVal s As String, ByVal l As Long)
            Return "String,Long"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
  Public Sub LateBind_Primitive_String10()
        Dim o As Object = New C18

        Assert.AreEqual("A,Long", o.F(i, i))
    End Sub

    Private Class C19
        Public Function F(ByVal a As A)
            Return "A"
        End Function

        Public Function F(ByVal b As BB)
            Return "BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_20()
        Dim o As Object = New C19

        Assert.AreEqual("BB", o.F(ccc))
    End Sub

    Private Class C20
        Public Function F(ByVal a As A, ByVal l As Long)
            Return "A,Long"
        End Function

        Public Function F(ByVal b1 As BB, ByVal i As Integer)
            Return "BB,Integer"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_21()
        Dim o As Object = New C20

        Assert.AreEqual("BB,Integer", o.F(ccc, i))
        Assert.AreEqual("BB,Integer", o.F(ccc, s))
        Assert.AreEqual("A,Long", o.F(ccc, l))
    End Sub

    Private Class C21
        Public Function F(ByVal s As Short, ByVal i As Integer)
            Return "Short,Integer"
        End Function

        Public Function F(ByVal s As Short, ByVal i As Long)
            Return "Short,Long"
        End Function

        Public Function F(ByVal s As Short, ByVal i As Integer, ByVal s2 As Short)
            Return "Short,Integer,Short"
        End Function

        Public Function F(ByVal s As Short, ByVal i As Long, ByVal i1 As Integer)
            Return "Short,Long,Integer"
        End Function

    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Primitive_50()
        Dim o As Object = New C21

        Assert.AreEqual("BB,Integer", o.F(i, i))

    End Sub

    Private Class C22
        Public Function F(ByVal i1 As Long, ByVal i2 As Integer) As String
            Return "Long,Integer"
        End Function

        Public Function F(ByVal i1 As Long, ByVal i2 As Long) As String
            Return "Long,Long"
        End Function

        Public Function F(ByVal i1 As Integer, ByVal i2 As Long) As String
            Return "Integer,Long"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_Primitive_51()
        Dim o As Object = New C22

        Assert.AreEqual("Long,Integer", o.F(i, i))

    End Sub

    Private Class C41
        Public Function F(ByVal a As A) As String
            Return "A"
        End Function

        Public Function F(ByVal a As A, ByVal ParamArray args() As A) As String
            Return "A,ParamArray A()"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray41()
        Dim o As Object = New C41

        Assert.AreEqual("A", o.F(ccA))
        Assert.AreEqual("A", o.F(ccB))
        Assert.AreEqual("A", o.F(ccc))
    End Sub

    Private Class C42
        Public Function F(ByVal a As Object) As String
            Return "Object"
        End Function

        Public Function F(ByVal a As A, ByVal ParamArray args() As A) As String
            Return "A,ParamArray A()"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray42()
        Dim o As Object = New C42

        Assert.AreEqual("A,ParamArray A()", o.F(ccA))
        Assert.AreEqual("A,ParamArray A()", o.F(ccB))
        Assert.AreEqual("A,ParamArray A()", o.F(ccc))

        Assert.AreEqual("A,ParamArray A()", o.F(ccA, ccA))
        Assert.AreEqual("A,ParamArray A()", o.F(ccc, ccB))
    End Sub

    Private Class C43
        Public Function F(ByVal a As A, ByVal b As Object) As String
            Return "A,Object"
        End Function

        Public Function F(ByVal a As Object, ByVal ParamArray args() As A) As String
            Return "Object,ParamArray A()"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Complex_ParamArray43()
        Dim o As Object = New C43

        o.F(ccA, ccA)
    End Sub

    Private Class C600
        Public Function F(ByVal ParamArray args() As Integer) As Integer
            Return args.Length
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray600()
        Dim o As Object = New C600
        Dim a As Integer() = {1, 2, 3}
        Dim x As Integer = 10

        Assert.AreEqual(3, o.F(a))
        Assert.AreEqual(4, o.F(x, x, x, x))
    End Sub

    Private Class C601
        Public Function F(ByVal ParamArray args() As Long) As Integer
            Return args.Length
        End Function
        Public Function F(ByVal ParamArray args() As Integer) As Integer
            Return args.Length
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray601()
        Dim o As Object = New C601
        Dim a As Long() = {1, 2, 3}

        Assert.AreEqual(3, o.F(a))
        Assert.AreEqual(4, o.F(10, 20, 30, 40))
    End Sub

    Private Class C602
        Function F(ByVal ParamArray a As Object()) As Integer
            Return 0
        End Function

        Function F()
            Return 1
        End Function

        Function F(ByVal a As Object, ByVal b As Object)
            Return 2
        End Function

        Function F(ByVal a As Object, ByVal b As Object, ByVal ParamArray c As Object())
            Return 3
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray602()
        Dim o As Object = New C602
        Assert.AreEqual(1, o.F())
        Assert.AreEqual(0, o.F(1))
        Assert.AreEqual(2, o.F(1, 2))
    End Sub

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_Complex_ParamArray603()
        Dim o As Object = New C602
        o.F(1, 2, 3)
    End Sub

    Class C604
        Function F(ByVal ParamArray args() As Integer) As Integer
            Dim a As Integer
            a = args.Length
            Return a
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Complex_ParamArray604()
        Dim o As Object = New C604
        Dim a As Integer() = {1, 2, 3}
        Dim b As Integer = 1
        Assert.AreEqual(3, o.F(a))
        Assert.AreEqual(4, o.F(10, b, 30, 40))
        Assert.AreEqual(0, o.F())
    End Sub

#Region "MS BUG"
    ' MS passes this test and fails in next that differs just by method ordering inside class
    ' we should fail in both cases
    Private Class C23
        Public Function F(ByVal i1 As Long, ByVal i2 As Integer, ByVal i3 As Long) As String
            Return "Long,Integer,Long"
        End Function

        Public Function F(ByVal i1 As Integer, ByVal i2 As Long, ByVal i3 As Integer) As String
            Return "Integer,Long,Integer"
        End Function

        Public Function F(ByVal i1 As Long, ByVal i2 As Integer, ByVal i3 As Integer) As String
            Return "Long,Integer,Integer"
        End Function
    End Class

    'THIS TEST SHOULD FAIL IN MS
    <Test(), ExpectedException(GetType(AmbiguousMatchException)), Category("NotDotNet")> _
   Public Sub LateBind_MS_Bug_NoFail()
        Dim o As Object = New C23

        Assert.AreEqual("Long,Integer,Integer", o.F(i, i, i))
    End Sub

    Private Class C24
        Public Function F(ByVal i1 As Integer, ByVal i2 As Long, ByVal i3 As Integer) As String
            Return "Integer,Long,Integer"
        End Function

        Public Function F(ByVal i1 As Long, ByVal i2 As Integer, ByVal i3 As Integer) As String
            Return "Long,Integer,Integer"
        End Function

        Public Function F(ByVal i1 As Long, ByVal i2 As Integer, ByVal i3 As Long) As String
            Return "Long,Integer,Long"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
   Public Sub LateBind_MS_Bug_Fail()
        Dim o As Object = New C24

        o.F(i, i, i)
    End Sub

#End Region

    Class C400
        Public Function fun(ByVal i As Integer, ByVal ParamArray a() As Long)
            Return 10
        End Function
        Public Function fun(ByVal ParamArray a() As Long)
            Return 20
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersM()
        Dim o As Object = New C400
        Dim a As Integer = o.fun(1, 2, 3)
        Assert.AreEqual(10, a)
    End Sub

    Class C401
        Public Function fun(ByVal i1 As Long, ByVal i2 As Long, ByVal i3 As Long)
            Return 10
        End Function
        Public Function fun(ByVal i1 As Integer, ByVal ParamArray a() As Long)
            Return 20
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersM_1()
        Dim o As Object = New C401
        Dim a As Integer = o.fun(1, 2, 3)
        Assert.AreEqual(20, a)
    End Sub

    Class C402
        Public Function fun(ByVal i1 As Integer, ByVal i2 As Long, ByVal ParamArray a() As Long)
            Return 10
        End Function
        Public Function fun(ByVal i1 As Integer, ByVal ParamArray a() As Long)
            Return 20
        End Function
    End Class

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_TypeMembersM_2()
        Dim o As Object = New C402
        o.fun(1, 2, 3)
    End Sub

    'TargetJvmNotWorking - Ambiguous matching in method resolution
    <Category("TargetJvmNotWorking")> _
    <Test()> _
    Public Sub LateBind_TypeMembersM_3()
        Dim o As Object = New C402
        o.fun(1, 2)
    End Sub

    Class C403
        Public Function fun(ByVal i1 As Short, ByVal i2 As Integer, ByVal ParamArray a() As Long)
            Return 10
        End Function
        Public Function fun(ByVal i1 As Short, ByVal ParamArray a() As Long)
            Return 20
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersM_4()
        Dim o As Object = New C403
        Dim sh As Short = 2
        Assert.AreEqual(10, o.fun(sh, sh, sh))
    End Sub

    Class C500
        Sub fun(ByRef a As Long)
            a = a + 10
        End Sub
        Sub fun(ByRef a As Integer)
            a = a + 20
        End Sub
        Sub fun(ByRef a As Decimal)
            a = a + 30
        End Sub
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersF()
        Dim a As Integer = 10
        Dim a1 As Long = 10
        Dim a2 As Decimal = 10
        Dim o As Object = New C500
        o.fun(a)
        o.fun(a1)
        o.fun(a2)

        Assert.AreEqual(30, a)
        Assert.AreEqual(20, a1)
        Assert.AreEqual(40, a2)
    End Sub

    Class C800
        Public Function fun(ByVal i As Integer, ByVal a As Long)
            If i = 2 And a = 1 Then
                Return 10
            End If
            Return 11
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersU()
        Dim o As Object = New C800
        Dim a As Integer = o.fun(a:=1, i:=2)
        Assert.AreEqual(10, a)
    End Sub

    Class C700
        Public b As Byte
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembersY()
        Dim o As Object = New C700
        o.b = 0

        Assert.AreEqual(0, o.b)
    End Sub

    Private Class C900
        Public Function F(ByVal ParamArray arr() As Integer) As String
            Return "ParamArray Integer()"
        End Function

        Public Function F(ByVal ParamArray arr() As Long) As String
            Return "ParamArray Long()"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_TypeMembers_2()
        Dim o As Object = New C900
        Dim i As Integer = 5
        Dim l As Long = 7
        Dim sh As Short = 3

        Assert.AreEqual("ParamArray Integer()", o.F(i, i, i, i, i))
        Assert.AreEqual("ParamArray Long()", o.F(l, l, l, l, l, l))
        Assert.AreEqual("ParamArray Integer()", o.F(sh, sh, sh, sh))
    End Sub

    Private Class C1000
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal i As Long)
            Return "Long"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_1()
        Dim o As Object = New C1000
        Assert.AreEqual("Integer", o.F(Nothing))
    End Sub

    Private Class C1001
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal i As Boolean)
            Return "Boolean"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_2()
        Dim o As Object = New C1001
        o.F(Nothing)
    End Sub

    Private Class C1002
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal i As Byte)
            Return "Byte"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_3()
        Dim o As Object = New C1002
        Assert.AreEqual("Byte", o.F(Nothing))
    End Sub

    Private Class C1003
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal i As Char)
            Return "Char"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_4()
        Dim o As Object = New C1003
        o.F(Nothing)
    End Sub

    Private Class C1004
        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function

        Public Function F(ByVal i As String)
            Return "String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_5()
        Dim o As Object = New C1004
        o.F(Nothing)
    End Sub

    Private Class C1005
        Public Function F(ByVal i As Char)
            Return "Char"
        End Function

        Public Function F(ByVal i As String)
            Return "String"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_6()
        Dim o As Object = New C1005
        Assert.AreEqual("Char", o.F(Nothing))
    End Sub

    Private Class C1006
        Public Function F(ByVal i As Char)
            Return "Char"
        End Function

        Public Function F(ByVal i As Boolean)
            Return "Boolean"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_7()
        Dim o As Object = New C1006
        Assert.AreEqual("Char", o.F(Nothing))
    End Sub

    Private Class C1007
        Public Function F(ByVal i As Boolean)
            Return "Boolean"
        End Function

        Public Function F(ByVal i As String)
            Return "String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_8()
        Dim o As Object = New C1007
        o.F(Nothing)
    End Sub

    Private Class C1008
        Public Function F(ByVal i As Object)
            Return "Object"
        End Function

        Public Function F(ByVal i As String)
            Return "String"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_9()
        Dim o As Object = New C1008
        Assert.AreEqual("String", o.F(Nothing))
    End Sub

    Private Class C1009
        Public Function F(ByVal i As Object)
            Return "Object"
        End Function

        Public Function F(ByVal i As Char)
            Return "Char"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_10()
        Dim o As Object = New C1009
        Assert.AreEqual("Char", o.F(Nothing))
    End Sub

    Private Class C1010
        Public Function F(ByVal i As Object)
            Return "Object"
        End Function

        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_11()
        Dim o As Object = New C1010
        Assert.AreEqual("Integer", o.F(Nothing))
    End Sub

    Private Class C1011
        Public Function F(ByVal i As A)
            Return "A"
        End Function

        Public Function F(ByVal i As Integer)
            Return "Integer"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_Nothing_12()
        Dim o As Object = New C1011
        o.F(Nothing)
    End Sub

    Private Class C1012
        Public Function F(ByVal i As A)
            Return "A"
        End Function

        Public Function F(ByVal i As BB)
            Return "BB"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_Nothing_13()
        Dim o As Object = New C1012
        Assert.AreEqual("BB", o.F(Nothing))
    End Sub

#Region "Private Helper Classes"

    Private Class A
        Public Overrides Function toString() As String
            Return "A.instance"
        End Function
    End Class

    Private Class BB
        Inherits A
        Public Overrides Function toString() As String
            Return "BB.instance"
        End Function
    End Class

    Private Class CC
        Inherits BB
        Public Overrides Function toString() As String
            Return "CC.instance"
        End Function
    End Class

    Private Class IC
        Implements IConvertible
        Public Function GetTypeCode() As System.TypeCode Implements System.IConvertible.GetTypeCode
            Return TypeCode.Object
        End Function

        Public Function ToBoolean(ByVal provider As System.IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean
            Return True
        End Function

        Public Function ToByte(ByVal provider As System.IFormatProvider) As Byte Implements System.IConvertible.ToByte
            Return 1
        End Function

        Public Function ToChar(ByVal provider As System.IFormatProvider) As Char Implements System.IConvertible.ToChar
            Return "a"c
        End Function

        Public Function ToDateTime(ByVal provider As System.IFormatProvider) As Date Implements System.IConvertible.ToDateTime
            Return New DateTime
        End Function

        Public Function ToDecimal(ByVal provider As System.IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal
            Return 1
        End Function

        Public Function ToDouble(ByVal provider As System.IFormatProvider) As Double Implements System.IConvertible.ToDouble
            Return 1
        End Function

        Public Function ToInt16(ByVal provider As System.IFormatProvider) As Short Implements System.IConvertible.ToInt16
            Return 1
        End Function

        Public Function ToInt32(ByVal provider As System.IFormatProvider) As Integer Implements System.IConvertible.ToInt32
            Return 1
        End Function

        Public Function ToInt64(ByVal provider As System.IFormatProvider) As Long Implements System.IConvertible.ToInt64
            Return 1
        End Function

        Public Function ToSByte(ByVal provider As System.IFormatProvider) As SByte Implements System.IConvertible.ToSByte
            Return New SByte
        End Function

        Public Function ToSingle(ByVal provider As System.IFormatProvider) As Single Implements System.IConvertible.ToSingle
            Return 1
        End Function

        Public Overloads Function ToString(ByVal provider As System.IFormatProvider) As String Implements System.IConvertible.ToString
            Return "ICOnvertible.instance"
        End Function

        Public Function ToType(ByVal conversionType As System.Type, ByVal provider As System.IFormatProvider) As Object Implements System.IConvertible.ToType
            Return New Object
        End Function

        Public Function ToUInt16(ByVal provider As System.IFormatProvider) As UInt16 Implements System.IConvertible.ToUInt16
            Return New UInt16
        End Function

        Public Function ToUInt32(ByVal provider As System.IFormatProvider) As UInt32 Implements System.IConvertible.ToUInt32
            Return New UInt32
        End Function

        Public Function ToUInt64(ByVal provider As System.IFormatProvider) As UInt64 Implements System.IConvertible.ToUInt64
            Return New UInt64
        End Function

    End Class
#End Region

End Class