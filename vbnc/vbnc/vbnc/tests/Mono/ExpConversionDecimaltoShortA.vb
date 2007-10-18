'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module ExpConversionDecimaltoShortA
    Function Main() As Integer
        Dim a As Decimal = 123.501
        Dim b As Short
        b = CShort(a)
        If b <> 124 Then
            System.Console.WriteLine("Decimal to Short Conversion is not working properly. Expected 124 but got " & b) : Return 1
        End If
    End Function
End Module
