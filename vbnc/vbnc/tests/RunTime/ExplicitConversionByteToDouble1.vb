Module ExplicitConversionByteToDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CByte(10)
        value2 = CDbl(value1)
        const2 = CDbl(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToDouble1")
            Return 1
        End If
    End Function
End Module

