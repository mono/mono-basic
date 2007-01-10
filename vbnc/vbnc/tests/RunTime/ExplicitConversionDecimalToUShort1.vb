Module ExplicitConversionDecimalToUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 90.09D
        value2 = CUShort(value1)
        const2 = CUShort(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToUShort1")
            Return 1
        End If
    End Function
End Module

