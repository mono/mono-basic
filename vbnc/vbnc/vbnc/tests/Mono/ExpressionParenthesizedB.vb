'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off

Imports System

Module ExpressionParenthesis
    Function Main() As Integer
        Dim y = 2
        Dim z = 3
        Dim x = (4 * (y + z)) ^ (4 / 2) * 5 + 5 * (z - y)
        If x <> 2005 Then
            Throw New Exception("Unexpected value for the Expression. x should be Equal to 2005 but got x=" & x)
        End If
    End Function
End Module
