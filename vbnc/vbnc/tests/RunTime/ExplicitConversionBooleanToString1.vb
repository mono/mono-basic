Module ExplicitConversionBooleanToString1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As String
        Dim const2 As String

        value1 = True
        value2 = CStr(value1)
        const2 = CStr(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToString1")
            Return 1
        End If
    End Function
End Module

