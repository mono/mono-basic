Module ExplicitConversionSByteToString1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As String
        Dim const2 As String

        value1 = CSByte(20)
        value2 = CStr(value1)
        const2 = CStr(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToString1")
            Return 1
        End If
    End Function
End Module

