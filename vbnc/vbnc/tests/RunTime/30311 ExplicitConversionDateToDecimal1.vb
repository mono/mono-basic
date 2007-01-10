Module ExplicitConversionDateToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = #01/01/2000 12:34#
        value2 = CDec(value1)
        const2 = CDec(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToDecimal1")
            Return 1
        End If
    End Function
End Module

