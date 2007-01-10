'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionStringtoShortA
    Function Main() As Integer
        Dim a As Short
        Dim b As String = "123"
        a = b
        If a <> 123 Then
            System.Console.WriteLine("Conversion of String to Short not working. Expected 123 but got " & a) : Return 1
        End If
    End Function
End Module
