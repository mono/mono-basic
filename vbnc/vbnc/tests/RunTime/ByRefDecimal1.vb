Module ByRefDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Decimal

        testvalue = 91.09D
        Tester(testvalue)
        result = testvalue = 90.09D

        If result = False Then
            System.Console.WriteLine("FAIL ByRefDecimal1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Decimal)
        value = 90.09D
    End Sub
End Module

