Class ArrayElements3
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim testvalue() As Integer = {1, 2}
        result = testvalue(1) = 2

       If result = False Then
           System.Console.WriteLine("FAIL ArrayElements3")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
   End Function
End Class
