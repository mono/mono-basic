Module ExplicitConversionLongToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 70L
        value2 = CInt(value1)
        const2 = CInt(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToInteger1")
            Return 1
        End If
    End Function
End Module

