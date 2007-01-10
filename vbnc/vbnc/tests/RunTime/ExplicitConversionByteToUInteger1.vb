Module ExplicitConversionByteToUInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CByte(10)
        value2 = CUInt(value1)
        const2 = CUInt(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToUInteger1")
            Return 1
        End If
    End Function
End Module

