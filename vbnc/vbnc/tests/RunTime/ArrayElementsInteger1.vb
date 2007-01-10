Module ArrayIntegerElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(4) As Integer

        testvalue(4) = 50
        result = testvalue(4) = 50

        If result = False Then
            System.Console.WriteLine("FAIL ArrayIntegerElements1")
            Return 1
        End If
    End Function

End Module

