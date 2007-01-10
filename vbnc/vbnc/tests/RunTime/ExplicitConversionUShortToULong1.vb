Module ExplicitConversionUShortToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 40US
        value2 = CULng(value1)
        const2 = CULng(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToULong1")
            Return 1
        End If
    End Function
End Module

