Module ImplicitConversionStringToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToInteger1")
            Return 1
        End If
    End Function
End Module

