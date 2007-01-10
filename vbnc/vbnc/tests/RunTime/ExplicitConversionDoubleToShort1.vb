Module ExplicitConversionDoubleToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Short
        Dim const2 As Short

        value1 = 110.011
        value2 = CShort(value1)
        const2 = CShort(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToShort1")
            Return 1
        End If
    End Function
End Module

