Module ExplicitConversionSByteToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CSByte(20)
        value2 = CULng(value1)
        const2 = CULng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToULong1")
            Return 1
        End If
    End Function
End Module

