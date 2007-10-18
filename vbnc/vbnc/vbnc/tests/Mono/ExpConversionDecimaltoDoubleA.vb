'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module ExpConversionDecimaltoDoubleA
    Function Main() As Integer
        Dim a As Decimal = 123.5
        Dim b As Double = CDbl(a)
        If b <> 123.5 Then
            System.Console.WriteLine("Explicit Conversion of Long to Double has Failed. Expected 123.5 but got " & b) : Return 1
        End If
    End Function
End Module
