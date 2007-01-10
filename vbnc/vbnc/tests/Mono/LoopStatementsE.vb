'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module Test
    Function Main() As Integer
        Dim i
        Dim s As String = "1881"
        Dim s1 As String = "1999"
        Dim s2 As String = "1"
        For i = s To s1 Step s2
        Next
    End Function
End Module
