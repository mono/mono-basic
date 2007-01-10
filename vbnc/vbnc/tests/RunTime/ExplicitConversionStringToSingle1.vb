Module ExplicitConversionStringToSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Single
        Dim const2 As Single

        value1 = "testvalue"
        value2 = CSng(value1)
        const2 = CSng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToSingle1")
            Return 1
        End If
    End Function
End Module

