Module ImplicitConversionByteToObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToObject1")
            Return 1
        End If
    End Function
End Module

