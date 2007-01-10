Module ExplicitConversionSingleToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 100.001!
        value2 = CSByte(value1)
        const2 = CSByte(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToSByte1")
            Return 1
        End If
    End Function
End Module

