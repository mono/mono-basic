Module ArrayObjectElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(15) As Object

        testvalue(15) = Nothing
        result = testvalue(15) Is Nothing

        If result = False Then
            System.Console.WriteLine("FAIL ArrayObjectElements1")
            Return 1
        End If
    End Function

End Module

