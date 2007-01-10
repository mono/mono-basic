Module ExplicitConversionDecimalToString1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As String
        Dim const2 As String

        value1 = 90.09D
        value2 = CStr(value1)
        const2 = CStr(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToString1")
            Return 1
        End If
    End Function
End Module

