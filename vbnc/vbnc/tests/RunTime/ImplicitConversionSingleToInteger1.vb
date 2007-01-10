Module ImplicitConversionSingleToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToInteger1")
            Return 1
        End If
    End Function
End Module

