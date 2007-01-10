Module ArrayUIntegerElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(5) As UInteger

        testvalue(5) = 60UI
        result = testvalue(5) = 60UI

        If result = False Then
            System.Console.WriteLine("FAIL ArrayUIntegerElements1")
            Return 1
        End If
    End Function

End Module

