'
' LateBindingTests4.vb
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
Public Class LateBindingTests4
    Private Class C1
        Public Function F(Optional ByVal i As Integer = -2, Optional ByVal l As Long = -1) As String
            If i = -2 Then
                If l = -1 Then
                    Return "Integer(optional value),Long(optional value)"
                Else
                    Return "Integer(optional value),Long"
                End If
            Else
                If l = -1 Then
                    Return "Integer,Long(optional value)"
                Else
                    Return "Integer,Long"
                End If
            End If
        End Function

        Public Function G(ByVal i As Integer, Optional ByVal l As Long = -1) As String
            If l = -1 Then
                Return "Integer,Long(optional value)"
            Else
                Return "Integer,Long"
            End If
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_OptionalValue_1()
        Dim o As Object = New C1

        Assert.AreEqual("Integer(optional value),Long(optional value)", o.F())
        Assert.AreEqual("Integer,Long(optional value)", o.F(2))
        Assert.AreEqual("Integer,Long", o.F(2, 3))
    End Sub

    <Test()> _
    Public Sub LateBind_OptionalValue_2()
        Dim o As Object = New C1

        Assert.AreEqual("Integer,Long(optional value)", o.G(2))
        Assert.AreEqual("Integer,Long", o.G(2, 3))
    End Sub

    <Test(), ExpectedException(GetType(System.MissingMemberException))> _
    Public Sub LateBind_OptionalValue_3()
        Dim o As Object = New C1

        o.G()
    End Sub

    Private Class C2
        Public Function F(ByRef j As Integer)
            j = 8
            Return "ByRef Integer"
        End Function
        Public Function F(ByVal i As Integer, ByRef j As Integer)
            j = i
            Return "Integer,ByRef Integer"
        End Function

        Public Function F(ByVal i As Integer, ByRef j As Integer, ByVal ParamArray arg() As Integer)
            j = arg(0) + arg.Length
            Return "Integer,ByRef Integer,ParmaArray Integer()"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ByRef_1()
        Dim o As Object = New C2
        Dim j As Integer = 15
        Dim i As Integer = 23

        Assert.AreEqual("ByRef Integer", o.F(j))
        Assert.AreEqual(8, j)

        Assert.AreEqual("Integer,ByRef Integer", o.F(i, j))
        Assert.AreEqual(i, j)

        Assert.AreEqual("Integer,ByRef Integer,ParmaArray Integer()", o.F(i, j, 7, 8, 9, 10))
        Assert.AreEqual(7 + 4, j)
    End Sub

    Private Class C3
        Public Function F(ByVal i As Integer, ByRef j() As Integer)
            j(2) = i
            Return "Integer,ByRef Integer()"
        End Function
        Public Function F(ByVal i() As Integer, ByRef j() As Integer)
            For k As Integer = 0 To j.Length - 1
                j(k) = i(k)
            Next
            Return "Integer(),ByRef Integer()"
        End Function

        Public Function F(ByVal i As Integer, ByRef j() As Integer, ByVal ParamArray arg() As Integer)
            For k As Integer = 0 To j.Length - 1
                j(k) = arg(k)
            Next
            Return "Integer,ByRef Integer(),ParmaArray Integer()"
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_ByRef_2()
        Dim o As Object = New C3
        Dim j() As Integer = {-1, -2, -3, -4, -5}
        Dim i() As Integer = {10, 20, 30, 40, 50, 60}
        Dim arg() As Integer = {1, 2, 3, 4, 5, 6, 7}

        Assert.AreEqual("Integer,ByRef Integer()", o.F(9, j))
        Assert.AreEqual(9, j(2))

        Assert.AreEqual("Integer(),ByRef Integer()", o.F(i, j))
        For k As Integer = 0 To j.Length - 1
            Assert.AreEqual(i(k), j(k))
        Next

        Assert.AreEqual("Integer,ByRef Integer(),ParmaArray Integer()", o.F(5, j, arg(0), arg(1), arg(2), arg(3), arg(4), arg(5), arg(6)))
        For k As Integer = 0 To j.Length - 1
            Assert.AreEqual(arg(k), j(k))
        Next
    End Sub

    Private Class C4

        Public Sub F(ByRef A() As Long)
            Dim J As Integer
            For J = 0 To 3
                A(J) = A(J) + 1
            Next J
        End Sub

        Public Sub G(ByRef A() As Long)
            Dim J As Integer
            Dim K() As Long = {100, 200, 300, 400}
            A = K
            For J = 0 To 3
                A(J) = A(J) + 1
            Next J
        End Sub
    End Class

    <Test()> _
    Public Sub LateBind_ByRef_3()
        Dim o As Object = New C4
        Dim N() As Long = {10, 20, 30, 40}
        Dim N1() As Long = {11, 21, 31, 41}
        Dim N2() As Long = {101, 201, 301, 401}
        Dim i As Integer

        o.F(N)
        For i = 0 To N.Length - 1
            Assert.AreEqual(N1(i), N(i))
        Next i

        o.G(N)
        For i = 0 To N.Length - 1
            Assert.AreEqual(N2(i), N(i))
        Next i
    End Sub


    Class C5
        Public Function F(ByVal i As Integer, Optional ByVal a1 As Char = "c", Optional ByVal j As Integer = 30) As Integer
            If a1 = "c" And i = 2 And j = 40 Then
                Return 10
            End If
            Return 11
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_NamedParam_1()
        Dim o As Object = New C5
        Dim a As Integer = o.F(j:=40, i:=2)
        Assert.AreEqual(10, a)
    End Sub

    Class C6
        Public Function F(ByRef i As Integer, Optional ByRef a1 As Char = "c", Optional ByRef j As Integer = 30) As Integer
            If a1 = "a" And i = 2 And j = 30 Then
                Return 10
            End If
            Return 11
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_NamedParam_2()
        Dim o As Object = New C6
        Dim a As Integer = o.F(a1:="a", i:=2)
        Assert.AreEqual(10, a)
    End Sub

    Class C7
        Public Function F(ByVal i As Integer, Optional ByVal a1 As Char = "d", Optional ByVal j As Integer = 30) As Integer
            If a1 = "c" And i = 2 And j = 30 Then
                Return 10
            End If
            Return 11
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_NamedParam_3()
        'Dim i As Integer
        Dim o As Object = New C7
        Dim a As Integer = o.F(a1:="caa", i:=2.321)
        Assert.AreEqual(10, a)
        'Assert.AreEqual(2, i)
    End Sub

    Class C8
        Public Function F(ByRef i As Integer, ByRef j As Integer) As Integer
            i = 9
            j = 10
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_NamedParam_4()
        Dim o As Object = New C8
        Dim a As Integer = 1
        Dim err As String = ""
        o.F(a, a)
        Assert.AreEqual(10, a)

        Assert.AreEqual(10, a)

        o.F(j:=a, i:=a)
        Assert.AreEqual(9, a)
    End Sub

    Class C9
        Public Function F(ByVal i As Integer, ByVal ParamArray arr() As Integer) As String
            Return "Integer,ParamArray Integer()"
        End Function

        Public Function F(ByVal i As Integer, ByVal arr As Integer) As String
            Return "Integer,Integer"
        End Function
    End Class

    <Test()> _
       Public Sub LateBind_NamedParam_5()
        Dim o As Object = New C9
        Assert.AreEqual("Integer,Integer", o.F(40, arr:=2))
    End Sub

    Class C10
        Public Function F(ByVal i As Integer, ByVal ParamArray arr() As Integer) As String
            Return "Integer,ParamArray Integer()"
        End Function
    End Class

    'TargetJvmNotWorking - ArgumentException was thrown when InvalidCastExceptions should be thrown
    <Category("TargetJvmNotWorking")> _
    <Test(), ExpectedException(GetType(InvalidCastException))> _
       Public Sub LateBind_NamedParam_6()
        Dim o As Object = New C10
        o.F(40, arr:=2)
    End Sub

    Class C11
        Public Function F(ByVal i As Integer, ByVal ParamArray arr() As Integer) As String
            Return "Integer,ParamArray Integer()"
        End Function

        Public Function F(ByVal i As Long, ByVal arr As Long) As String
            Return "Long,Long"
        End Function

        Public Function F(ByVal i As Integer, ByVal j As Integer) As String
            Return "Integer,Integer"
        End Function
    End Class

    <Test()> _
       Public Sub LateBind_NamedParam_7()
        Dim o As Object = New C11
        Assert.AreEqual("Long,Long", o.F(40, arr:=2))
    End Sub

    Class C12
        Public Function F(ByVal i As Long, ByVal arr As Long) As String
            Return "Long,Long"
        End Function

        Public Function F(ByVal i As Integer, ByVal arr As Integer) As String
            Return "Integer,Integer"
        End Function
    End Class

    <Test()> _
       Public Sub LateBind_NamedParam_8()
        Dim o As Object = New C12
        Assert.AreEqual("Integer,Integer", o.F(40, arr:=2))
    End Sub

    Class C13
        Public Function F(ByVal i As Integer, ByVal j As Long) As String
            Return "Integer,Long"
        End Function

        Public Function F(ByVal j As Integer, ByVal i As Integer) As String
            Return "Integer,Integer"
        End Function
    End Class

    <Test()> _
       Public Sub LateBind_NamedParam_9()
        Dim o As Object = New C13
        Assert.AreEqual("Integer,Integer", o.F(40, i:=2))
    End Sub

    Class C14
        Public Function F(ByVal ParamArray arr() As Integer) As String
            Return "ParamArray Integer()"
        End Function

        Public Function F(ByVal arr As String) As String
            Return "String"
        End Function
    End Class

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
       Public Sub LateBind_NamedParam_10()
        Dim o As Object = New C14
        Dim iarr() As Integer = {5, 6, 7}
        o.F(arr:=iarr)
    End Sub

    'TargetJvmNotWorking - MissingMemberException was thrown when InvalidCastExceptions should be thrown
    <Category("TargetJvmNotWorking")> _
    <Test(), ExpectedException(GetType(InvalidCastException))> _
       Public Sub LateBind_NamedParam_11()
        Dim o As Object = New C14
        Dim iarr() As Integer = {5, 6, 7}
        o.F(40, iarr)
    End Sub

    <Test()> _
      Public Sub LateBind_NamedParam_16()
        Dim o As Object = New C14
        Dim iarr() As Integer = {5, 6, 7}
        Assert.AreEqual("ParamArray Integer()", o.F(iarr))
    End Sub


    Class C15
        Public Function F(ByVal i As Integer, ByVal j As Integer, ByVal ParamArray arr() As Integer) As String
            Return "Integer,Integer,ParamArray Integer()"
        End Function

        Public Function F(ByVal i As Integer, ByVal j As Integer, ByVal arr As String) As String
            Return "Integer,Integer,String"
        End Function
    End Class

#If TARGET_JVM Then
    <Test(), ExpectedException(GetType(InvalidCastException)),Category("TargetJvmNotWorking")> Public Sub LateBind_NamedParam_12()
#Else
    <Test(), ExpectedException(GetType(InvalidCastException))> Public Sub LateBind_NamedParam_12()
#End If
        Dim o As Object = New C15
        Assert.AreEqual("Integer,Integer,ParamArray Integer()", o.F(i:=5, j:=6))
    End Sub

    'TargetJvmNotWorking - InvalidCastExceptions should be thrown
    <Category("TargetJvmNotWorking")> _
    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Public Sub LateBind_NamedParam_15()
        Dim o As Object = New C15
        Assert.AreEqual("Integer,Integer,ParamArray Integer()", o.F(i:=5, j:=6))
    End Sub

    <Test()> _
    Public Sub LateBind_NamedParam_13()
        Dim o As Object = New C15
        Assert.AreEqual("Integer,Integer,String", o.F(i:=5, j:=6, arr:=Nothing))
    End Sub

    <Test(), ExpectedException(GetType(AmbiguousMatchException))> _
    Public Sub LateBind_NamedParam_14()
        Dim o As Object = New C15
        Dim iarr() As Integer = {5, 6, 7}
        Assert.AreEqual("Integer,Integer,String", o.F(i:=5, j:=6, arr:=iarr))
    End Sub

End Class