Module ImplicitConversionBooleanToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Date
        Dim const2 As Date

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToDate1")
            Return 1
        End If
    End Function
End Module

