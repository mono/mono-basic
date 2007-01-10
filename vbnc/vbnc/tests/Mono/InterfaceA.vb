Option Strict Off
Interface I
    Function F()
    Function S(ByVal i As Integer)
    Property P()
    Event e(ByVal i As Integer)
End Interface

Class C
    Implements I

    Function F() Implements I.F
    End Function

    Function S(ByVal i As Integer) Implements I.S
    End Function

    Property P() Implements I.P
        Get
        End Get
        Set(ByVal value)
        End Set
    End Property

    Event e(ByVal i As Integer) Implements I.e
End Class

Module InterfaceA
    Function Main() As Integer
        Dim x As C = New C()
        x.F()

        Dim y As I = New C()
        y.F()
    End Function
End Module
