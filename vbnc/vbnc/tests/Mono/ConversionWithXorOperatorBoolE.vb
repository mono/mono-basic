'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionXorOperator

    Function Main() As Integer
        Dim A As Integer = 333
        Dim B As Integer = 555
        Dim R As Boolean
        R = (B > A) Xor (A < B)
        If R = -1 Then
            System.Console.WriteLine("#Error With Xor Operator") : Return 1
        End If
    End Function
End Module
