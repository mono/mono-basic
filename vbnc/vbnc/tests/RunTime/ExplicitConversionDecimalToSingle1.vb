Module ExplicitConversionDecimalToSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Single
        Dim const2 As Single

        value1 = 90.09D
        value2 = CSng(value1)
        const2 = CSng(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToSingle1")
            Return 1
        End If
    End Function
End Module

