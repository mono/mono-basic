Module ExplicitConversionObjectToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = Nothing
        value2 = CSByte(value1)
        const2 = CSByte(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToSByte1")
            Return 1
        End If
    End Function
End Module

