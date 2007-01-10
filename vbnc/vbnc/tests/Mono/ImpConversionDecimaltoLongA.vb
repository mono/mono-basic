'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDecimaltoLongA
    Function Main() As Integer
        Dim a As Decimal = 123.501
        Dim b As Long = a
        If b <> 124 Then
            System.Console.WriteLine("Implicit Conversion of Long to Single has Failed. Expected 123.5 but got " & b) : Return 1
        End If
    End Function
End Module
