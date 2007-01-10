'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDoubletoDecimalC
    Function Main() As Integer
        Dim a As Double = 111.9
        Dim b As Decimal = 111.9 + a
        If b <> 223.8 Then
            System.Console.WriteLine("Addition of Double & Decimal not working. Expected 223.8 but got " & b) : Return 1
        End If
    End Function
End Module
