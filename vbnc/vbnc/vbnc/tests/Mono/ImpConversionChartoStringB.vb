'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionChartoStringB
    Function Main() As Integer
        Dim a() As Char = "Program"
        Dim b As String = a
        If b <> "Program" Then
            System.Console.WriteLine("Conversion of Char to String not working. Expected Program but got " & b) : Return 1
        End If
    End Function
End Module
