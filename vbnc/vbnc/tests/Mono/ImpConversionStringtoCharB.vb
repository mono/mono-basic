'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionStringtoCharB
    Function Main() As Integer
        Dim a() As Char
        Dim b As String = "Program"
        a = b
        If a <> "Program" Then
            System.Console.WriteLine("Conversion of String to Char not working. Expected Program but got " & a) : Return 1
        End If
    End Function
End Module
