Class JaggedArrayElements2
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim values()() As Integer
        values = New Integer()() {New Integer(1) {1, 2}}
        result = values(0)(1) = 2

       If result = False Then
           System.Console.WriteLine("FAIL JaggedArrayElements2")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
   End Function
End Class
