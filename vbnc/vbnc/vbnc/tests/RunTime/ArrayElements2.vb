Class ArrayElements2
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim value(0, 3) As Integer
        value(0, 0) = 2
        value(0, 1) = value(0, 0) + 2
        result = value(0, 1) = 4

       If result = False Then
           System.Console.WriteLine("FAIL ArrayElements2")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
   End Function
End Class
