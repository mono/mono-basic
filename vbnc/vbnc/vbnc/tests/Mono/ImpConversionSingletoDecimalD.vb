'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionSingletoDecimalC
    Function Main() As Integer
        Dim a As Single = 111
        Dim b As Decimal = 111.9 + a
        If b <> 222.9 Then
            System.Console.WriteLine("Addition of Single & Decimal not working. Expected 222.9 but got " & b) : Return 1
        End If
    End Function
End Module
