Module ExplicitConversionDecimalToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 90.09D
        value2 = CInt(value1)
        const2 = CInt(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToInteger1")
            Return 1
        End If
    End Function
End Module

