'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionChartoStringB
    Function Main() As Integer
        Dim a As Char = "a"
        'Dim b as String = "hello" + a
        Dim b As String = a + "hello"
        If b <> "ahello" Then
            System.Console.WriteLine("Concat of Char & String not working. Expected helloa but got " & b) : Return 1
        End If
    End Function
End Module


