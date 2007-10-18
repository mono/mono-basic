'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionInttoShortA
    Function Main() As Integer
        Dim a As Integer = 123
        Dim b As Short
        b = a
        If b <> 123 Then
            System.Console.WriteLine("Int to Short Conversion is not working properly. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
