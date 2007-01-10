Module ExplicitConversionSByteToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = CSByte(20)
        value2 = CByte(value1)
        const2 = CByte(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToByte1")
            Return 1
        End If
    End Function
End Module

