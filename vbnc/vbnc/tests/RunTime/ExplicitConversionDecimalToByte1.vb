Module ExplicitConversionDecimalToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 90.09D
        value2 = CByte(value1)
        const2 = CByte(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToByte1")
            Return 1
        End If
    End Function
End Module

