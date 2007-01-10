Class ArrayElements5
    Shared Function Main() As Integer
        Dim result As Boolean

        Dim values(0) As test
        Dim value As test
        result = False
        values(0) = value
        values(0).value = 1
        result = values(0).value = 1

        If result = False Then
            System.Console.WriteLine("FAIL ArrayElements5")
            System.Console.WriteLine("(detailed message)")
            Return 1
        End If
    End Function
    Structure Test
        Public Value As Integer
    End Structure
End Class
