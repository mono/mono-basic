Module ImplicitConversionULongToString1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As String
        Dim const2 As String

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToString1")
            Return 1
        End If
    End Function
End Module

