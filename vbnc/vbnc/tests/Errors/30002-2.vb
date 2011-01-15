Public Class C
    Public Sub B()
    End Sub
End Class
Public Class A
    Inherits C

    Function F() As A.B
    End Function
End Class