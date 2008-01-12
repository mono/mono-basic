'
' LateBindingTests6.vb
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
Public Class LateBindingTests6
    Class C100
        Overridable Function fun(ByVal j As Integer)
            Return -1
        End Function
    End Class

    Class C200
        Inherits C100
        Overrides Function fun(ByVal j As Integer)
            i = j
            Return i
        End Function
        Public i As Integer = 2
    End Class

    <Test()> _
    Public Sub LateBind_OverrideA()
        Dim a As Object = New C200
        Assert.AreEqual(2, a.fun(a.i))
    End Sub

    Class B100
        Overridable Function F() As Integer
            Return 5
        End Function
    End Class

    Class D100
        Inherits B100

        Overrides Function F() As Integer
            ' you should be able to access 
            ' the members of base class 
            ' using 'MyBase' as follows
            MyBase.F()

            Return 10
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_OverrideB()
        Dim x As Object

        x = New B100
        Assert.AreEqual(5, x.F())

        x = New D100
        Assert.AreEqual(10, x.F())
    End Sub

    Class base100
        Default Public Overridable ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return i
            End Get
        End Property
    End Class

    Class derive100
        Inherits base100
        Default Public Overrides ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return 2 * i
            End Get
        End Property
    End Class

    <Test()> _
    Public Sub LateBind_OverrideC()
        Dim a As Object = New derive100
        Assert.AreEqual(20, a(10))
    End Sub

    Class base200
        Public Overridable ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return i
            End Get
        End Property
    End Class

    Class derive200
        Inherits base200
        Public Overrides ReadOnly Property Item(ByVal i As Integer) As Integer
            Get
                Return 2 * i
            End Get
        End Property
    End Class

    <Test()> _
    Public Sub LateBind_OverrideD()
        Dim a As Object = New derive200
        Assert.AreEqual(20, a.Item(10))
    End Sub

    Class base300
        Public Overridable Property Item(ByVal i As Integer) As Integer
            Get
                Return i
            End Get
            Set(ByVal Value As Integer)
            End Set
        End Property
    End Class

    Class derive300
        Inherits base300
        Public Overrides Property Item(ByVal i As Integer) As Integer
            Get
                Return 2 * i
            End Get
            Set(ByVal Value As Integer)
            End Set
        End Property
    End Class

    <Test()> _
    Public Sub LateBind_OverrideE()
        Dim a As Object = New derive300
        Assert.AreEqual(20, a.Item(10))
    End Sub

    Class C5
        Private i As Integer = 20

        Public Property p() As Integer
            Get
                Return i
            End Get

            Set(ByVal val As Integer)
                i = val
            End Set

        End Property
    End Class

    <Test()> _
    Public Sub LateBind_PropertyA()
        Dim o As Object = New C5
        Assert.AreEqual(20, o.p)
    End Sub

    <Test()> _
   Public Sub LateBind_PropertyB()
        Dim o As Object = New C5
        o.p = 10
        Assert.AreEqual(10, o.p)
    End Sub

    Class C6
        Public Shared y As Integer = 20
        Public z As Integer = 30

        Shared Sub New()
        End Sub

        Public Sub New()
        End Sub

        Shared Function f() As Integer
            Return 50
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_VariablesA()
        Assert.AreEqual(20, C6.y)

        Dim c As Object = New C6
        Dim d As Object = New C6

        Assert.AreEqual(20, c.y)
        Assert.AreEqual(20, d.y)

        C6.y = 25
        Assert.AreEqual(25, c.y)

        c.y = 35
        Assert.AreEqual(35, C6.y)
        Assert.AreEqual(35, d.y)
        Assert.AreEqual(30, c.z)
        Assert.AreEqual(50, C6.f)
    End Sub

    Class A100
        Public Shared i As Integer
    End Class

    <Test()> _
    Public Sub LateBind_VariablesB()
        Dim o As Object = New A100
        o.i = o.i + 1
        A100.i = A100.i + 1
        Assert.AreEqual(2, A100.i)
    End Sub

    Class A200
        Public i As Integer
        Sub New()
            i = 20
        End Sub
        Sub New(ByVal a As A200)
            i = a.i
        End Sub
    End Class

    <Test()> _
    Public Sub LateBind_VariablesC()
        Dim a As Object = New A200
        Dim j As Object = New A200(a)
        Assert.AreEqual(20, j.i)
    End Sub

    Class A300
        Inherits System.MarshalByRefObject
        Public Function fun()
            Return -1
        End Function
    End Class

    Class A400
        Public Function fun(ByVal a As A300)
            Return 1
        End Function
    End Class

    <Test()> _
    Public Sub LateBind_VariablesD()
        Dim b As Object = New A300
        Dim a As Object = New A400
        Assert.AreEqual(1, a.fun(b))
    End Sub

    Class C300
        Public a1 As Integer = 10
        Public a2 As String = "Hello"
        Sub f1()

        End Sub
    End Class


    <Test()> _
    Public Sub LateBind_IndexOfReturnObjectFromProperty1()
        Dim o As Object
        Dim res As Object
        o = New A
        res = o.dodo(0)
        Assert.AreEqual("1", res)
    End Sub

    <Test()> _
    Public Sub LateBind_IndexOfReturnObjectFromProperty2()
        Dim o As Object
        Dim res As Object
        o = New B
        res = o.dodo(0)
        Assert.AreEqual("3", res)
    End Sub

    Public Class A
        Public ReadOnly Property dodo() As Object()
            Get
                Return New Object() {"1", "2"}
            End Get
        End Property
    End Class

    Public Class B
        Public ReadOnly Property dodo() As C
            Get
                Return New C
            End Get
        End Property
    End Class

    Public Class C
        Default Public ReadOnly Property Blubber(ByVal i As Integer) As Object
            Get
                Return "3"
            End Get
        End Property
    End Class

    <Test()> _
    Public Sub LateBind_WithStatementA()
        Dim a As Object = New C300
        Dim bRes1 As Boolean = False
        Dim bRes2 As Boolean = False

        With a
            .a2 = "Hello World"
            GoTo labelA
            ' Exit before all statements in With have been executed  
            Throw New Exception("Exit before all statements in With have been executed")
labelA:
        End With
        bRes1 = False
        If (a.a1 = 20) Then bRes1 = True
        Assert.AreEqual(False, bRes1)

    End Sub
End Class