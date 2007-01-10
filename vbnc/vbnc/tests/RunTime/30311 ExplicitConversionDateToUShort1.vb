Module ExplicitConversionDateToUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = #01/01/2000 12:34#
        value2 = CUShort(value1)
        const2 = CUShort(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToUShort1")
            Return 1
        End If
    End Function
End Module

