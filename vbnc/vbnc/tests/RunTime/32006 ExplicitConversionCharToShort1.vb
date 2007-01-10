Module ExplicitConversionCharToShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Short
        Dim const2 As Short

        value1 = "C"c
        value2 = CShort(value1)
        const2 = CShort("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToShort1")
            Return 1
        End If
    End Function
End Module

