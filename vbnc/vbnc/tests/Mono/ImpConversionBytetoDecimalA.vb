'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ImpConversionBytetoDecimalA
    Function Main() As Integer
        Dim a As Byte = 123
        Dim b As Decimal
        b = a
        If b <> 123 Then
            System.Console.WriteLine("Byte to Decimal Conversion is not working properly. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
