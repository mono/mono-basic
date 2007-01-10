Module ExplicitConversionStringToLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Long
        Dim const2 As Long

        value1 = "testvalue"
        value2 = CLng(value1)
        const2 = CLng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToLong1")
            Return 1
        End If
    End Function
End Module

