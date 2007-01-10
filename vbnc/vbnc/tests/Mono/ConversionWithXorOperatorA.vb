'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ConversionXorOperator

    Function Main() As Integer
        Dim A As Integer = 444
        Dim B As Long = 333.333
        Dim C As Double = 222.222
        Dim D As Short = 111.111
        Dim R As Boolean
        R = ((A > B) Xor (C > A)) Xor ((B < C) Or (D > C))
        If R = False Then
            System.Console.WriteLine("#Error With Xor Operator") : Return 1
        End If
    End Function
End Module

