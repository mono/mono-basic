Module ExplicitConversionSingleToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 100.001!
        value2 = CULng(value1)
        const2 = CULng(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToULong1")
            Return 1
        End If
    End Function
End Module

