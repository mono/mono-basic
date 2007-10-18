'
' LateBindingTests5.vb
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
Public Class LateBindingTests5

    Private Class C1
        Public a() As Integer = {1, 2, 3, 4, 5}
    End Class

    <Test()> _
    Public Sub LateBind_ArrayC()
        Dim c As Integer
        Dim o As Object = New C1
        c = UBound(o.a, 1)
        Assert.AreEqual(4, c)
        c = LBound(o.a, 1)
        Assert.AreEqual(0, c)
    End Sub

    Class base
        Default Public ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return i
            End Get
        End Property
    End Class

    Class derive
        Inherits base
        Public Shadows ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return 2 * i
            End Get
        End Property
    End Class

    Class derive1
        Inherits derive
        Default Public Shadows ReadOnly Property Item1(ByVal i As Integer) As Integer
            Get
                Return 3 * i
            End Get
        End Property
    End Class

    <Test()> _
    Public Sub LateBind_DefaultPropD()
        Dim a As Object = New derive1
        Dim b As derive = a
        Dim i, j, k As Integer
        i = a(10)
        j = a.Item(10)
        k = b(10)
        Assert.AreEqual(30, i)
        Assert.AreEqual(20, j)
        Assert.AreEqual(10, k)
    End Sub

    Class C2
        Public F As Integer = 10
    End Class

    Private Function ReturnC() As Object
        Return New C2
    End Function

    <Test()> _
    Public Sub LateBind_ExpressionMemberAccess()
        Assert.AreEqual(10, ReturnC().F)
    End Sub


    Private Shared i3 As Integer = 0
    Class C3
        Sub f1()
            i3 += 1
            f2()
        End Sub

        Function f2() As Integer
            i3 += 2
        End Function

        Function f2(ByVal i As Integer) As Integer
            i3 += 5
            Return i3
        End Function

        Function f2(ByVal o As Object) As Boolean
            Return True
        End Function

        Function f3(ByVal j As Integer) As Integer
            i3 += j
        End Function

        Function f4(ByVal j As Integer) As Integer
            i3 += j * 10
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_InvocationStatement()
        Dim obj As Object = New C3
        Call obj.f1()
        Assert.AreEqual(3, i3)
        Assert.AreEqual(8, obj.f2(i3))
        Assert.IsTrue(obj.f2("Hello"))
        Assert.IsTrue(obj.f2(2.3D))
    End Sub


    Class C4
        Function A(ByVal i As Integer) As Integer
            Return i
        End Function
        Function AB() As Integer
            Return 10
        End Function
    End Class

    <Category("TargetJvmNotWorking"), Test()> _
    Public Sub LateBind_MethodDeclarationA()
        Dim o As Object = New C4
        Assert.AreEqual(10, o.A(o.AB()))
    End Sub

End Class