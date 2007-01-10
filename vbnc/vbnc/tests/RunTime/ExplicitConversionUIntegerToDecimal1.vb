Module ExplicitConversionUIntegerToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 60UI
        value2 = CDec(value1)
        const2 = CDec(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToDecimal1")
            Return 1
        End If
    End Function
End Module

