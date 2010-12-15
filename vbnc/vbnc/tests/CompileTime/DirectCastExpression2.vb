Class DirectCastExpression2
    Enum E
        A
    End Enum
    Sub Test()
        DirectCast(1, Integer).tostring()
        Dim e As E = DirectCast(1, E)
    End Sub
End Class