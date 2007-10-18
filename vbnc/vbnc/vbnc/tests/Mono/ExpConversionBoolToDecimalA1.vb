Module ExpConversionBoolToDecimal
    Function Main() As Integer
        Dim a As Boolean = False
        Dim b As Decimal = CDec(a)
        If b <> 0 Then
            System.Console.WriteLine("Boolean to Decimal Conversion failed. Expected 0 but got " & b) : Return 1
        End If
    End Function
End Module