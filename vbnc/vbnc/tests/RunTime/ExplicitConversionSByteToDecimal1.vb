Module ExplicitConversionSByteToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = CSByte(20)
        value2 = CDec(value1)
        const2 = CDec(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDecimal1")
            Return 1
        End If
    End Function
End Module

