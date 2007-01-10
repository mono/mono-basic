Module ExplicitConversionDecimalToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Char
        Dim const2 As Char

        value1 = 90.09D
        value2 = CChar(value1)
        const2 = CChar(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToChar1")
            Return 1
        End If
    End Function
End Module

