Module ExplicitConversionStringToSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "testvalue"
        value2 = CSByte(value1)
        const2 = CSByte("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToSByte1")
            Return 1
        End If
    End Function
End Module

