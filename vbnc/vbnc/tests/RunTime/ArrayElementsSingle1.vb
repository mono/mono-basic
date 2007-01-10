Module ArraySingleElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(9) As Single

        testvalue(9) = 100.001!
        result = testvalue(9) = 100.001!

        If result = False Then
            System.Console.WriteLine("FAIL ArraySingleElements1")
            Return 1
        End If
    End Function

End Module

