'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionNotOperator

    Function Main() As Integer
        Dim A As Integer = 333
        Dim B As Long = 555.555
        Dim R As Boolean
        R = Not (A > B)
        If R = False Then
            System.Console.WriteLine("#Error With Not Operator") : Return 1
        End If
    End Function
End Module
