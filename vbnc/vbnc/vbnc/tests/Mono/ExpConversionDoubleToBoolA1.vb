Module ExpConversionDoubletoBool
    Function Main() As Integer
        Dim a As Double = -4.94065645841247E-324
        Dim b As Boolean
        b = CBool(a)
        If b <> True Then
            System.Console.WriteLine("Double to Boolean Conversion is not working properly. Expected True but got " & b) : Return 1
        End If
    End Function
End Module
