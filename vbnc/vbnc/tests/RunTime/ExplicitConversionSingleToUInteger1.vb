Module ExplicitConversionSingleToUInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 100.001!
        value2 = CUInt(value1)
        const2 = CUInt(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToUInteger1")
            Return 1
        End If
    End Function
End Module

