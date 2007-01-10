Class AndAlsoExpression2
    Shared Function Main() As Integer
        Dim lvalue, rvalue As Object
        Dim result As Boolean
        result = CBool(lvalue) AndAlso CBool(rvalue)
        If result Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class