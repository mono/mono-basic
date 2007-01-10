Module ExplicitConversionLongToUInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 70L
        value2 = CUInt(value1)
        const2 = CUInt(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToUInteger1")
            Return 1
        End If
    End Function
End Module

