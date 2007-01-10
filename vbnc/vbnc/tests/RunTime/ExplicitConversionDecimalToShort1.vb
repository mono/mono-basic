Module ExplicitConversionDecimalToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Short
        Dim const2 As Short

        value1 = 90.09D
        value2 = CShort(value1)
        const2 = CShort(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToShort1")
            Return 1
        End If
    End Function
End Module

