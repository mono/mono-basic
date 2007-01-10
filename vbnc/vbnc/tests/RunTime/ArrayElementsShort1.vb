Module ArrayShortElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(2) As Short

        testvalue(2) = 30
        result = testvalue(2) = 30

        If result = False Then
            System.Console.WriteLine("FAIL ArrayShortElements1")
            Return 1
        End If
    End Function

End Module

