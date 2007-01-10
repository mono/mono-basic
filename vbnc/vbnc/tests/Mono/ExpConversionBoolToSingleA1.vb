Module ExpConversionofBoolToSingle
    Function Main() As Integer
        Dim a As Boolean = False
        Dim b As Single = CSng(a)
        If b <> 0 Then
            System.Console.WriteLine("Explicit Conversion of Bool(Fals):return 1 to Single has Failed. Expected 0, but got " & b) : Return 1
        End If
    End Function
End Module
