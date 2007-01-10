Class JaggedArrayElements1
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim values()() As Integer = {New Integer(2) {}}
        values(0)(2) = 1
        result = values(0)(2) = 1

       If result = False Then
           System.Console.WriteLine("FAIL JaggedArrayElements1")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
   End Function
End Class
