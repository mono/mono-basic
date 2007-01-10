Module ExplicitConversionCharToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "C"c
        value2 = CInt(value1)
        const2 = CInt("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToInteger1")
            Return 1
        End If
    End Function
End Module

