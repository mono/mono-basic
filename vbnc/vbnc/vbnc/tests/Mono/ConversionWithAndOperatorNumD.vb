'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionAndOperator

    Function Main() As Integer
        Dim A As Integer = 10
        Dim B As Integer = 9
        Dim R As Boolean
        R = A And B     '1010 And 1001 
        If R = False Then
            System.Console.WriteLine("#Error With And Operator") : Return 1
        End If
    End Function
End Module

