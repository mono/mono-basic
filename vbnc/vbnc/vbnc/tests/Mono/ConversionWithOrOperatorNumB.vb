'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionOrOperator

    Function Main() As Integer
        Dim A As Integer = 10
        Dim B As Integer = 9
        Dim R As Boolean
        R = A Or B  '1010 Or 1001 
        If R = False Then
            System.Console.WriteLine("#Error With Or Operator") : Return 1
        End If
    End Function
End Module
