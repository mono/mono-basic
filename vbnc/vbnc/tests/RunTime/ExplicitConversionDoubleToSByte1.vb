Module ExplicitConversionDoubleToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 110.011
        value2 = CSByte(value1)
        const2 = CSByte(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToSByte1")
            Return 1
        End If
    End Function
End Module

