Module ImplicitConversionStringToBoolean1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToBoolean1")
            Return 1
        End If
    End Function
End Module

