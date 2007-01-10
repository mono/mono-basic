Module ExplicitConversionSByteToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CSByte(20)
        value2 = CDate(value1)
        const2 = CDate(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDate1")
            Return 1
        End If
    End Function
End Module

