Module ExplicitConversionStringToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Short
        Dim const2 As Short

        value1 = "testvalue"
        value2 = CShort(value1)
        const2 = CShort("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToShort1")
            Return 1
        End If
    End Function
End Module

