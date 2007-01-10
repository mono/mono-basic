Class PropertyAccessExpression3
    Shared Function Main() As Integer
        Dim v As System.Nullable(Of Integer)
        If v.HasValue Then
            Return v.Value
        Else
            Return 0
        End If
    End Function
End Class