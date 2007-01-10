Module ExplicitConversionSingleToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Date
        Dim const2 As Date

        value1 = 100.001!
        value2 = CDate(value1)
        const2 = CDate(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToDate1")
            Return 1
        End If
    End Function
End Module

