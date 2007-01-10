Module ExplicitConversionCharToDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Double
        Dim const2 As Double

        value1 = "C"c
        value2 = CDbl(value1)
        const2 = CDbl("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToDouble1")
            Return 1
        End If
    End Function
End Module

