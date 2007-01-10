Module ExplicitConversionULongToLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Long
        Dim const2 As Long

        value1 = 80UL
        value2 = CLng(value1)
        const2 = CLng(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToLong1")
            Return 1
        End If
    End Function
End Module

