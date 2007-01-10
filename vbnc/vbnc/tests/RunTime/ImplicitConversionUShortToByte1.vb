Module ImplicitConversionUShortToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToByte1")
            Return 1
        End If
    End Function
End Module

