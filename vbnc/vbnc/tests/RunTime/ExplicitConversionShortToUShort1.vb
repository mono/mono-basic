Module ExplicitConversionShortToUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 30S
        value2 = CUShort(value1)
        const2 = CUShort(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToUShort1")
            Return 1
        End If
    End Function
End Module

