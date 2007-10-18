'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionStringtoDecimalA
    Function Main() As Integer
        Dim a As Decimal
        Dim b As String = "123052"
        a = b
        If a <> 123052 Then
            System.Console.WriteLine("Conversion of String to Decimal not working. Expected 123052 but got " & a) : Return 1
        End If
    End Function
End Module
