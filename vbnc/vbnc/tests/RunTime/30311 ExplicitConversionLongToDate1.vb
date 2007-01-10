Module ExplicitConversionLongToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Date
        Dim const2 As Date

        value1 = 70L
        value2 = CDate(value1)
        const2 = CDate(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToDate1")
            Return 1
        End If
    End Function
End Module

