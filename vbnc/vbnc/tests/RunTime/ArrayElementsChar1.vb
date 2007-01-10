Module ArrayCharElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(13) As Char

        testvalue(13) = "C"c
        result = testvalue(13) = "C"c

        If result = False Then
            System.Console.WriteLine("FAIL ArrayCharElements1")
            Return 1
        End If
    End Function

End Module

