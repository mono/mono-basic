Option Strict Off
Interface I
    Function F1(ByVal i As Integer)
    Function F2(ByVal i As Integer)
End Interface

Class B
    Implements I

    Overridable Function CF1(ByVal i As Integer) Implements I.F1
    End Function

    Overridable Function CF2(ByVal i As Integer) Implements I.F2
    End Function
End Class

Class D
    Inherits B
    Overrides Function CF2(ByVal i As Integer)
    End Function
End Class

Module InterfaceE
    Function Main() As Integer
        Dim x As D = New D()
        x.CF1(10)
        x.CF2(10)
    End Function
End Module


