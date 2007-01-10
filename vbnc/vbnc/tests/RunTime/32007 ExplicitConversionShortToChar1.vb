Module ExplicitConversionShortToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Char
        Dim const2 As Char

        value1 = 30S
        value2 = CChar(value1)
        const2 = CChar(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToChar1")
            Return 1
        End If
    End Function
End Module

