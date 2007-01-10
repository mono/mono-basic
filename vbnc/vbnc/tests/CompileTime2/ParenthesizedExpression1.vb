Class ParenthesizedExpression1
    Shared Function Main() As Integer
        Dim i As Integer = 2
        Dim j As Integer = (i)
        If j <> 2 Then
            system.console.writeline("FAIL ParenthesizedExpression1")
        End If
    End Function
End Class