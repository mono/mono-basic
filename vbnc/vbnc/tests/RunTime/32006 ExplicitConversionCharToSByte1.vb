Module ExplicitConversionCharToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "C"c
        value2 = CSByte(value1)
        const2 = CSByte("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToSByte1")
            Return 1
        End If
    End Function
End Module

