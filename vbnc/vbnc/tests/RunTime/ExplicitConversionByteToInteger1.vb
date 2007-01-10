Module ExplicitConversionByteToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = CByte(10)
        value2 = CInt(value1)
        const2 = CInt(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToInteger1")
            Return 1
        End If
    End Function
End Module

