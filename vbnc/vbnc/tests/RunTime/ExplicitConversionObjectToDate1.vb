Module ExplicitConversionObjectToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Date
        Dim const2 As Date

        value1 = Nothing
        value2 = CDate(value1)
        const2 = CDate(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToDate1")
            Return 1
        End If
    End Function
End Module

