Module ExplicitConversionByteToSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CByte(10)
        value2 = CSng(value1)
        const2 = CSng(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToSingle1")
            Return 1
        End If
    End Function
End Module

