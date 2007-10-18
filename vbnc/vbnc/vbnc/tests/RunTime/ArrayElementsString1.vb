Module ArrayStringElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(11) As String

        testvalue(11) = "testvalue"
        result = testvalue(11) = "testvalue"

        If result = False Then
            System.Console.WriteLine("FAIL ArrayStringElements1")
            Return 1
        End If
    End Function

End Module

