Module ExplicitConversionULongToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Char
        Dim const2 As Char

        value1 = 80UL
        value2 = CChar(value1)
        const2 = CChar(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToChar1")
            Return 1
        End If
    End Function
End Module

