Module ExplicitConversionULongToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Object
        Dim const2 As Object

        value1 = 80UL
        value2 = CObj(value1)
        const2 = CObj(80UL)

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToObject1")
            Return 1
        End If
    End Function
End Module

