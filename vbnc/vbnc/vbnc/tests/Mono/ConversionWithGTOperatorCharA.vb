'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ExpressionOperatorChar

    Function Main() As Integer
        Dim A As Char = "a"
        Dim B As Char = "b"
        If A > B Then
            System.Console.WriteLine("Exception occured Value of a Should be less than b ") : Return 1
        End If
    End Function
End Module
