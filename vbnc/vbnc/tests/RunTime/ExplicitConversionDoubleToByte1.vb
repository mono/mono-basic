Module ExplicitConversionDoubleToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 110.011
        value2 = CByte(value1)
        const2 = CByte(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToByte1")
            Return 1
        End If
    End Function
End Module

