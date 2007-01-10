Module ExplicitConversionBooleanToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Char
        Dim const2 As Char

        value1 = True
        value2 = CChar(value1)
        const2 = CChar(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToChar1")
            Return 1
        End If
    End Function
End Module

