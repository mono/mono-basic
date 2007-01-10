'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ConversionXorOperator

    Function Main() As Integer
        Dim A As Integer = 10
        Dim B As Integer = 9
        Dim R As Integer
        R = A Xor B     '1010 XOr 1001 
        If R <> 3 Then
            System.Console.WriteLine("#Error With Xor Operator") : Return 1
        End If
    End Function
End Module
