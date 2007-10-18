Class MethodExpression1
    Shared Function Test() As Integer
        Return 0
    End Function
    Shared Function Main() As Integer
        Dim value As Integer
        value = Test 'Without parenthesis.
        Return value
    End Function
End Class