Module ExplicitConversionSingleToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 100.001!
        value2 = CDec(value1)
        const2 = CDec(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToDecimal1")
            Return 1
        End If
    End Function
End Module

