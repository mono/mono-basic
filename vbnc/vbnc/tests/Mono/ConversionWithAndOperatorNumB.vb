'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionAndOperator

    Function Main() As Integer
        Dim A As Integer = 3
        Dim B As Boolean = False
        Dim R As Boolean
        R = A And B     '0101 And 0 = 0000 
        If R = True Then
            System.Console.WriteLine("#Error With And Operator") : Return 1
        End If
    End Function
End Module

