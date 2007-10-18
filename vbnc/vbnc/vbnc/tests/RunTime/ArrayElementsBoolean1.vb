Module ArrayBooleanElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(12) As Boolean

        testvalue(12) = True
        result = testvalue(12) = True

        If result = False Then
            System.Console.WriteLine("FAIL ArrayBooleanElements1")
            Return 1
        End If
    End Function

End Module

