Class A
    Public c As Integer = 10
End Class

Class B
    Inherits A

    Public Shadows c As Integer = 20
End Class

Module M
    Function Main() As Integer
    End Function
End Module
