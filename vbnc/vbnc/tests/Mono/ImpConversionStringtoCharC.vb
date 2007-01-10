'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionStringtoCharD
    Function Main() As Integer
        Dim a As String = "hello"
        Dim b As Char = a + "a"
        If b <> "h" Then
            System.Console.WriteLine("Concat of String & Char not working. Expected  'h' but got " & b) : Return 1
        End If
    End Function
End Module

