Module ExplicitConversionStringToDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Double
        Dim const2 As Double

        value1 = "testvalue"
        value2 = CDbl(value1)
        const2 = CDbl("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToDouble1")
            Return 1
        End If
    End Function
End Module

