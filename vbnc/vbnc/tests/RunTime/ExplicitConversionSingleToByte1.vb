Module ExplicitConversionSingleToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 100.001!
        value2 = CByte(value1)
        const2 = CByte(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToByte1")
            Return 1
        End If
    End Function
End Module

