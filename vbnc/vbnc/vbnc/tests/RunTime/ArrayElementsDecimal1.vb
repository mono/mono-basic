Module ArrayDecimalElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(8) As Decimal

        testvalue(8) = 90.09D
        result = testvalue(8) = 90.09D

        If result = False Then
            System.Console.WriteLine("FAIL ArrayDecimalElements1")
            Return 1
        End If
    End Function

End Module

