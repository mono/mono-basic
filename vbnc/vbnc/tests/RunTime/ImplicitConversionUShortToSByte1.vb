Module ImplicitConversionUShortToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToSByte1")
            Return 1
        End If
    End Function
End Module

