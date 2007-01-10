Module ExplicitConversionByteToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CByte(10)
        value2 = CULng(value1)
        const2 = CULng(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToULong1")
            Return 1
        End If
    End Function
End Module

