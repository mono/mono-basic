'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off

Imports System

Module ExpressionParenthesized
    Function Main() As Integer
        Dim x = 4 ^ -2 * 128 / 2.0 \ 2 Mod 2 + 5 - 2 << 1 >> 1
        Dim y = (((((((((4 ^ (-2)) * 128) / 2.0) \ 2) Mod 2) + 5) - 2) << 1) >> 1)
        Dim z = 3
        If x <> z Then
            Throw New Exception("Unexpected value for Expression. x should be Equal to 3 but Got x = " & x)
        End If
        If y <> x Then
            Throw New Exception("Unexpected value for Expression. expected y = x = 3 but got y = " & y)
        End If
    End Function
End Module

