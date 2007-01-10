Module ExplicitConversionULongToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 80UL
        value2 = CDec(value1)
        const2 = CDec(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToDecimal1")
            Return 1
        End If
    End Function
End Module

