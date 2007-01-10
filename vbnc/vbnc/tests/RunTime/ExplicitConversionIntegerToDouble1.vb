Module ExplicitConversionIntegerToDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Double
        Dim const2 As Double

        value1 = 50I
        value2 = CDbl(value1)
        const2 = CDbl(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToDouble1")
            Return 1
        End If
    End Function
End Module

