Module ExplicitConversionIntegerToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 50I
        value2 = CULng(value1)
        const2 = CULng(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToULong1")
            Return 1
        End If
    End Function
End Module

