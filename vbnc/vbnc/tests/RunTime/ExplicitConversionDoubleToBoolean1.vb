Module ExplicitConversionDoubleToBoolean1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 110.011
        value2 = CBool(value1)
        const2 = CBool(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToBoolean1")
            Return 1
        End If
    End Function
End Module

