Module ExplicitConversionIntegerToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Char
        Dim const2 As Char

        value1 = 50I
        value2 = CChar(value1)
        const2 = CChar(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToChar1")
            Return 1
        End If
    End Function
End Module

