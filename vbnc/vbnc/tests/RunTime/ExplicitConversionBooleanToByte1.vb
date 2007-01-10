Module ExplicitConversionBooleanToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = True
        value2 = CByte(value1)
        const2 = CByte(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToByte1")
            Return 1
        End If
    End Function
End Module

