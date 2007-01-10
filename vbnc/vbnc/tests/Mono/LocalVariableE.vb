'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module M
    Function Main() As Integer
        Static x As Date
        If x <> "1/1/0001" Then
            System.Console.WriteLine("Static declaration not implemented properly. Expected 1/1/0001 but got " & x) : Return 1
        End If
    End Function
End Module
