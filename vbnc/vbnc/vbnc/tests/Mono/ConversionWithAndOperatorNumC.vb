'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionAndOperator

    Function Main() As Integer
        Dim A As Integer = 0
        Dim B As Integer = 2
        Dim R As Boolean
        R = A And B     '00 And 10 
        If R = True Then
            System.Console.WriteLine("#Error With And Operator") : Return 1
        End If
    End Function
End Module
