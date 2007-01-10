Class IsNotExpression2
    Shared Function Main() As Integer
        Dim o As IsNotExpression2

        If o IsNot Nothing Then
            Return 1
        ElseIf o Is Nothing Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class