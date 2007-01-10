Module ExplicitConversionDecimalToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 90.09D
        value2 = CULng(value1)
        const2 = CULng(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToULong1")
            Return 1
        End If
    End Function
End Module

