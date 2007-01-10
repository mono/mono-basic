Module ExplicitConversionShortToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 30S
        value2 = CInt(value1)
        const2 = CInt(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToInteger1")
            Return 1
        End If
    End Function
End Module

