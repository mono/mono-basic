Module ExplicitConversionStringToByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "testvalue"
        value2 = CByte(value1)
        const2 = CByte("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToByte1")
            Return 1
        End If
    End Function
End Module

