Module ExplicitConversionObjectToInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = Nothing
        value2 = CInt(value1)
        const2 = CInt(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToInteger1")
            Return 1
        End If
    End Function
End Module

