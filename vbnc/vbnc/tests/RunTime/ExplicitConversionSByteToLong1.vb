Module ExplicitConversionSByteToLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Long
        Dim const2 As Long

        value1 = CSByte(20)
        value2 = CLng(value1)
        const2 = CLng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToLong1")
            Return 1
        End If
    End Function
End Module

