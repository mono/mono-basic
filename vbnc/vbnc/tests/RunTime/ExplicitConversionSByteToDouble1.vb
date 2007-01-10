Module ExplicitConversionSByteToDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CSByte(20)
        value2 = CDbl(value1)
        const2 = CDbl(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDouble1")
            Return 1
        End If
    End Function
End Module

