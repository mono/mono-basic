'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ExpressionOperator1

    Function Main() As Integer
        Dim L As Long = 100.555
        Dim I As Integer = 100
        If L = I Then
            System.Console.WriteLine("# Error L can't be equal to I ") : Return 1
        End If
    End Function
End Module
