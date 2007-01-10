Module ExplicitConversionSingleToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Short
        Dim const2 As Short

        value1 = 100.001!
        value2 = CShort(value1)
        const2 = CShort(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToShort1")
            Return 1
        End If
    End Function
End Module

