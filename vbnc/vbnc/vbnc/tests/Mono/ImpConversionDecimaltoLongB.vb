'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDecimaltoLongC
    Function Main() As Integer
        Dim a As Decimal = 111.9
        Dim b As Long = 111.9 + a
        If b <> 224 Then
            System.Console.WriteLine("Addition of Decimal & Long not working. Expected 224 but got " & b) : Return 1
        End If
    End Function
End Module

