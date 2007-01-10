'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionLeftShiftOperator

    Function Main() As Integer
        Dim A As Long = 10.666
        Dim B As Integer = 2
        Dim R As Integer
        R = A << B
        If R <> 44 Then
            System.Console.WriteLine("#Error With << Shift Operator") : Return 1
        End If
    End Function
End Module
