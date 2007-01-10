Module ImplicitConversionObjectToBoolean1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToBoolean1")
            Return 1
        End If
    End Function
End Module

