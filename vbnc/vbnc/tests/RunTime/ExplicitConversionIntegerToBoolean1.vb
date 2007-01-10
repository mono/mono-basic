Module ExplicitConversionIntegerToBoolean1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 50I
        value2 = CBool(value1)
        const2 = CBool(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToBoolean1")
            Return 1
        End If
    End Function
End Module

