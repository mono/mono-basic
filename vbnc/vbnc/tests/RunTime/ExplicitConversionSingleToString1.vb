Module ExplicitConversionSingleToString1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As String
        Dim const2 As String

        value1 = 100.001!
        value2 = CStr(value1)
        const2 = CStr(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToString1")
            Return 1
        End If
    End Function
End Module

