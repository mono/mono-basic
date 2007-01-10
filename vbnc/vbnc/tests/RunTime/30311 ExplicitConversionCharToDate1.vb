Module ExplicitConversionCharToDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Date
        Dim const2 As Date

        value1 = "C"c
        value2 = CDate(value1)
        const2 = CDate("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToDate1")
            Return 1
        End If
    End Function
End Module

