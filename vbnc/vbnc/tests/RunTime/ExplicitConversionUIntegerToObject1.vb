Module ExplicitConversionUIntegerToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Object
        Dim const2 As Object

        value1 = 60UI
        value2 = CObj(value1)
        const2 = CObj(60UI)

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToObject1")
            Return 1
        End If
    End Function
End Module

