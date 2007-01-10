Module ExplicitConversionSByteToUInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CSByte(20)
        value2 = CUInt(value1)
        const2 = CUInt(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToUInteger1")
            Return 1
        End If
    End Function
End Module

