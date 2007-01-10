Module ExplicitConversionSByteToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Short
        Dim const2 As Short

        value1 = CSByte(20)
        value2 = CShort(value1)
        const2 = CShort(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToShort1")
            Return 1
        End If
    End Function
End Module

