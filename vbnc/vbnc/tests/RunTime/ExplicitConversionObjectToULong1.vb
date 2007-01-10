Module ExplicitConversionObjectToULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = Nothing
        value2 = CULng(value1)
        const2 = CULng(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToULong1")
            Return 1
        End If
    End Function
End Module

