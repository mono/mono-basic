Module ExplicitConversionBooleanToLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Long
        Dim const2 As Long

        value1 = True
        value2 = CLng(value1)
        const2 = CLng(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToLong1")
            Return 1
        End If
    End Function
End Module

