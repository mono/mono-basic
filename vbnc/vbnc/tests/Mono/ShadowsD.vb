Option Strict Off
Class B
    Public Shared Function F()
    End Function
End Class

Class D
    Inherits B

    Private Shared Shadows Function F()
    End Function
End Class

Class D1
    Inherits D
End Class

Module ShadowsD
    Function Main() As Integer
        Dim x As New D1()
        x.F()
    End Function
End Module
