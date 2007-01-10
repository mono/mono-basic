Module ImplicitConversionShortToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToSByte1")
            Return 1
        End If
    End Function
End Module

