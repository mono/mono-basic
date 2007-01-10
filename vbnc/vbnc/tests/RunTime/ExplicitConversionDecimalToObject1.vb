Module ExplicitConversionDecimalToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Object
        Dim const2 As Object

        value1 = 90.09D
        value2 = CObj(value1)
        const2 = CObj(90.09D)

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToObject1")
            Return 1
        End If
    End Function
End Module

