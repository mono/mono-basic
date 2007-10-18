Public Class Test
    Public Shared Function Main() As Integer
        Dim value As Integer
        Dim result As Integer
        result = 1
        Select Case Value
            Case 0
                If True Then
                    result = 0
                    Exit Select
                End If
            Case 1
        End Select
        Return result
    End Function
End Class