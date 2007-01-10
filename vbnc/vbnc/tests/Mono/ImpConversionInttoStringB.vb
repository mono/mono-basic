'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionInttoStringC
    Function Main() As Integer
        Dim a As Integer = 123
        Dim b As String = a + "123"
        If b <> "246" Then
            System.Console.WriteLine("Concat of Int & String not working. Expected 246 but got " & b) : Return 1
        End If
    End Function
End Module

