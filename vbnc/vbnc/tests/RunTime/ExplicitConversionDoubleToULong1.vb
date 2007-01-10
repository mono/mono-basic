Module ExplicitConversionDoubleToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 110.011
        value2 = CULng(value1)
        const2 = CULng(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToULong1")
            Return 1
        End If
    End Function
End Module

