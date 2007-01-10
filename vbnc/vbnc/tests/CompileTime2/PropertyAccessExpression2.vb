Class PropertyAccessExpression2
    Shared ReadOnly Property Test() As Integer
        Get
            Return 3
        End Get
    End Property
    Shared Function Main() As Integer
        Return Test() - 3
    End Function
End Class