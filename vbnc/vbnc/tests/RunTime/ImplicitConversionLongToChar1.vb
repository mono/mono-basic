Module ImplicitConversionLongToChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Char
        Dim const2 As Char

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToChar1")
            Return 1
        End If
    End Function
End Module

