Module ImplicitConversionCharToUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToUShort1")
            Return 1
        End If
    End Function
End Module

