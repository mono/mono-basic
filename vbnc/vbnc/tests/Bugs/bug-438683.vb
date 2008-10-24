Imports system
Class C
    Shared Function main() As Integer
        Dim s As String = Nothing
        Dim result As Boolean

        result = Not s Is Nothing AndAlso False

        result = Not result

        If result Then
            Console.writeline("SUCCESS")
            Return 0
        Else
            Console.writeline("FAIL")
            Return 1
        End If
    End Function
End Class