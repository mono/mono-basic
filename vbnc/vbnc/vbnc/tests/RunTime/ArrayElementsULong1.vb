Module ArrayULongElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(7) As ULong

        testvalue(7) = 80UL
        result = testvalue(7) = 80UL

        If result = False Then
            System.Console.WriteLine("FAIL ArrayULongElements1")
            Return 1
        End If
    End Function

End Module

