'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ConversionXorOperator

    Function Main() As Integer
        Dim A As Integer = 124
        Dim B As Byte = 254
        Dim R As Boolean
        R = (B < A) Xor (A < B)
        If R = False Then
            System.Console.WriteLine("#Error With Xor Operator") : Return 1
        End If
    End Function
End Module
