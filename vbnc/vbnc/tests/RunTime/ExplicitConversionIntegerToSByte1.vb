Module ExplicitConversionIntegerToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 50I
        value2 = CSByte(value1)
        const2 = CSByte(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToSByte1")
            Return 1
        End If
    End Function
End Module

