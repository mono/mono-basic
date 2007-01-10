Class ByRef3
   Shared Function Main() As Integer
       Dim result As Boolean

        Dim testvalue As Integer
        tester((testvalue))
        result = testvalue = 0

       If result = False Then
           System.Console.WriteLine("FAIL ByRef3")
           System.Console.WriteLine("(detailed message)")
           Return 1
       End If
    End Function
    Shared Sub Tester(ByRef value As Integer)
        value += 1
    End Sub
End Class
