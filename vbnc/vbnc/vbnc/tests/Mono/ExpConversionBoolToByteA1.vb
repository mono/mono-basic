Module ExpConversionBoolToByte
    Function Main() As Integer
        Dim a As Boolean = False
        Dim b As Byte = CByte(a)
        If b <> 0 Then
            System.Console.WriteLine("Boolean to Byte Conversion failed. Expected 255 but got " & b) : Return 1
        End If
    End Function
End Module