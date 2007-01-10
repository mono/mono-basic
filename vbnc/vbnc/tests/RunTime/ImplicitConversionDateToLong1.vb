Module ImplicitConversionDateToLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Long
        Dim const2 As Long

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToLong1")
            Return 1
        End If
    End Function
End Module

