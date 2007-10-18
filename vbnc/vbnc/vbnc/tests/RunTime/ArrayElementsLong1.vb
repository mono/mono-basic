Module ArrayLongElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(6) As Long

        testvalue(6) = 70
        result = testvalue(6) = 70

        If result = False Then
            System.Console.WriteLine("FAIL ArrayLongElements1")
            Return 1
        End If
    End Function

End Module

