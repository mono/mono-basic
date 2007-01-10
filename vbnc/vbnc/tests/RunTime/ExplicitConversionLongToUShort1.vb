Module ExplicitConversionLongToUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 70L
        value2 = CUShort(value1)
        const2 = CUShort(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToUShort1")
            Return 1
        End If
    End Function
End Module

