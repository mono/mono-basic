Module ExplicitConversionDoubleToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 110.011
        value2 = CInt(value1)
        const2 = CInt(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToInteger1")
            Return 1
        End If
    End Function
End Module

