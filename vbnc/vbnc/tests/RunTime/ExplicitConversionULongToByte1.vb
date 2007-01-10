Module ExplicitConversionULongToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 80UL
        value2 = CByte(value1)
        const2 = CByte(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToByte1")
            Return 1
        End If
    End Function
End Module

