Module ArraySByteElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(1) As SByte

        testvalue(1) = 20
        result = testvalue(1) = 20

        If result = False Then
            System.Console.WriteLine("FAIL ArraySByteElements1")
            Return 1
        End If
    End Function

End Module

