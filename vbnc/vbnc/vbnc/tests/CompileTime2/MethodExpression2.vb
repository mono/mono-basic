Class MethodExpression2
    Shared Function Test1() As Integer
        Return 3
    End Function
    Shared Function Test2() As Integer
        Return 4
    End Function

    Shared Function Main() As Integer
        Return test1 * 4 - test2 * 3
    End Function
End Class