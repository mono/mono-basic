Module ExplicitConversionDoubleToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 110.011
        value2 = CDec(value1)
        const2 = CDec(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToDecimal1")
            Return 1
        End If
    End Function
End Module

