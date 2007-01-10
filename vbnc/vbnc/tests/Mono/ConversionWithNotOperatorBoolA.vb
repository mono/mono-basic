'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ConversionNotOperator

    Function Main() As Integer
        Dim A As Integer = 29
        Dim R As Integer
        R = Not (A)
        If R <> -30 Then
            System.Console.WriteLine("#Error With Not Operator") : Return 1
        End If
    End Function
End Module

