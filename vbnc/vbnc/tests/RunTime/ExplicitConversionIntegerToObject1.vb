Module ExplicitConversionIntegerToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Object
        Dim const2 As Object

        value1 = 50I
        value2 = CObj(value1)
        const2 = CObj(50I)

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToObject1")
            Return 1
        End If
    End Function
End Module

