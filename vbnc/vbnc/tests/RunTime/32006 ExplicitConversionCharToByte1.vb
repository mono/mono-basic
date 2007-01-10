Module ExplicitConversionCharToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "C"c
        value2 = CByte(value1)
        const2 = CByte("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToByte1")
            Return 1
        End If
    End Function
End Module

