Module ExplicitConversionStringToDecimal1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = "testvalue"
        value2 = CDec(value1)
        const2 = CDec("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToDecimal1")
            Return 1
        End If
    End Function
End Module

