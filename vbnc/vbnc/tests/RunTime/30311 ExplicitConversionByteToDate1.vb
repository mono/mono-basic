Module ExplicitConversionByteToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CByte(10)
        value2 = CDate(value1)
        const2 = CDate(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToDate1")
            Return 1
        End If
    End Function
End Module

