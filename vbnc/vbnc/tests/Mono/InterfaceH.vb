' In this test all implemented
' member names are different from 
' the interface member names

Delegate Sub d()

Interface I
    Function F() As Object
    Sub S(ByVal i As Integer)
    Property P() As Object
    Event e(ByVal i As Integer)
    Event e1 As d
End Interface

Class C
    Implements I

    Function CF() As Object Implements I.F
    End Function

    Sub CS(ByVal i As Integer) Implements I.S
    End Sub

    Sub S1(ByVal i As Integer)
    End Sub


    Property CP() As Object Implements I.P
        Get
        End Get
        Set(ByVal value As Object)
        End Set
    End Property

    Event Ce(ByVal i As Integer) Implements I.e

    Event Ce1 As d Implements I.e1

End Class

Module InterfaceA
    Function Main() As Integer
        Dim x As C = New C()
        x.CF()

        Dim y As I = New C()
        y.F()
    End Function
End Module
