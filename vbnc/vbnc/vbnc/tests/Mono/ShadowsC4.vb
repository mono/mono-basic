Option Strict Off
Class B
    Private Function F()
    End Function
End Class

Class D
    Inherits B

    Shadows Function F()
    End Function
End Class

Module ShadowsC3
    Function Main() As Integer
    End Function
End Module
