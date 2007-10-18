Class ArrayElements1
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim arrvalue(0) As Integer
        Dim value As Integer
        value = arrvalue(0)
        result = value = 0

        If result = False Then
            System.Console.WriteLine("FAIL ArrayElements1")
            System.Console.WriteLine("(detailed message)")
            Return 1
        End If
   End Function
End Class
