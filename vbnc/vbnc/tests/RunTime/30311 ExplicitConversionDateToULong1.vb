Module ExplicitConversionDateToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = #01/01/2000 12:34#
        value2 = CULng(value1)
        const2 = CULng(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToULong1")
            Return 1
        End If
    End Function
End Module

