Option Strict Off
Class MultipleExpression1
    Shared Function Main() As Integer
        Dim value As Double
        value = 3
        If value = Not 3 Then Return 1
    End Function
End Class