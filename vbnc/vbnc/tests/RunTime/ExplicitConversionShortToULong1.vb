Module ExplicitConversionShortToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 30S
        value2 = CULng(value1)
        const2 = CULng(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToULong1")
            Return 1
        End If
    End Function
End Module

