Module ExpConversionofBoolToLong
    Function Main() As Integer
        Dim a As Boolean = True
        Dim b As Long = CLng(a)
        If b <> -1 Then
            System.Console.WriteLine("Explicit Conversion of Bool(Tru):return 1 to Long has Failed. Expected -1, but got " & b) : Return 1
        End If
    End Function
End Module
