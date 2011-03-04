Option Strict Off

Class GenericType3A(Of X As C)
    Shared Sub GenericMethodTypeArgument(ByVal a As X)
        Dim o As D
        o = a
    End Sub
End Class

Class C
End Class
Class D
    Inherits C
End Class