Module ExplicitConversionByteToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CByte(10)
        value2 = CObj(value1)
        const2 = CObj(CByte(10))

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToObject1")
            Return 1
        End If
    End Function
End Module

