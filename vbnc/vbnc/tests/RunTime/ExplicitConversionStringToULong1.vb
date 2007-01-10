Module ExplicitConversionStringToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = "testvalue"
        value2 = CULng(value1)
        const2 = CULng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToULong1")
            Return 1
        End If
    End Function
End Module

