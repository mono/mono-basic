'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionXorOperator

    Function Main() As Integer
        Dim A As Integer = 3
        Dim B As Boolean = True
        Dim R As Boolean
        R = A Xor B     '0000 XOr -1 = 1111 
        If R = False Then
            System.Console.WriteLine("#Error With Xor Operator") : Return 1
        End If
    End Function
End Module
