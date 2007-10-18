'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off

Imports System

Module ExpressionParenthesized
    Function Main() As Integer
        Dim a = 4 ^ -2 * 3
        Dim b = (4 ^ (-2)) * 3
        Dim x = 4 ^ -2 * 128 / 2.0 \ 2 Mod 2 + 5 - 2 << 1 >> 1
        Dim y = (((((((((4 ^ (-2)) * 128) / 2.0) \ 2) Mod 2) + 5) - 2) << 1) >> 1)
        Dim z = 3
        Dim result As Integer
        If x <> z Then
            System.Console.WriteLine("Unexpected value for Expression. x should be Equal to 3 but Got x = " & x)
            result += 1
        End If
        If y <> x Then
            System.Console.WriteLine("Unexpected value for Expression. expected y = x = 3 but got y = " & y)
            result += 1
        End If
        Return result
    End Function
End Module

