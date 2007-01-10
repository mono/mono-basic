Module ExplicitConversionSByteToSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CSByte(20)
        value2 = CSng(value1)
        const2 = CSng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToSingle1")
            Return 1
        End If
    End Function
End Module

