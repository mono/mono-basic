Module ExpConversionofBoolToShort
    Function Main() As Integer
        Dim a As Boolean = False
        Dim b As Short = CShort(a)
        If b <> 0 Then
            System.Console.WriteLine("Explicit Conversion of Bool(Fals):return 1 to Shorthas Failed. Expected 0, but got " & b) : Return 1
        End If
    End Function
End Module
