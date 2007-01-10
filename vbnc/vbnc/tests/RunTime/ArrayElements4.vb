Class ArrayElements4
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim testvalue(,) As Integer = {{1, 2, 3}, {4, 2, 0}}
        result = testvalue(1, 0) = 4


       If result = False Then
           System.Console.WriteLine("FAIL ArrayElements4")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
   End Function
End Class
