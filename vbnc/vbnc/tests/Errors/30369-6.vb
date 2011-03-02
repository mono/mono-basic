Option Strict On
Option Explicit On

Public Class C
    Public Shared Sub SharedMethodA(ByVal sender As Object, ByVal e As EventArgs)
        Console.WriteLine("curious!")
    End Sub

    Public Shared Function Foo() As Integer
        Return &H45
    End Function

    Public Shared Property Bar() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As int32)
        End Set
    End Property

    Public Shared Event WizBangChanged As EventHandler

    Public Shared m_foo As Int32 = 99
End Class

Class C1
    Dim C As C

    Shared Sub Accesses()
        Console.WriteLine(C.m_foo)
        Console.WriteLine(C.Foo())
        Dim eh As New EventHandler(AddressOf C.SharedMethodA)
        AddHandler C.WizBangChanged, eh
        C.Bar = 100
        Console.WriteLine(C.Bar)
    End Sub
End Class

Class C2
    Dim F As C

    ReadOnly Property C As C
        Get
            Return Nothing
        End Get
    End Property

    Function M() As C
        Return Nothing
    End Function

    Shared Sub Accesses()
        Console.WriteLine(C.Foo())
    End Sub
End Class

Class C3
    Dim F As C

    ReadOnly Property C As C
        Get
            Return Nothing
        End Get
    End Property

    Function M() As C
        Return Nothing
    End Function

    Shared Sub Accesses()
        Console.WriteLine(C.Foo())
    End Sub
End Class