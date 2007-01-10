Module ExplicitConversionDoubleToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Date
        Dim const2 As Date

        value1 = 110.011
        value2 = CDate(value1)
        const2 = CDate(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToDate1")
            Return 1
        End If
    End Function
End Module

