Module ArrayByteElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(0) As Byte

        testvalue(0) = 10
        result = testvalue(0) = 10

        If result = False Then
            System.Console.WriteLine("FAIL ArrayByteElements1")
            Return 1
        End If
    End Function

End Module

