'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ConversionXOrOperator

    Function Main() As Integer
        Dim A As Integer = 0
        Dim B As Integer = 2
        Dim R As Integer
        R = A Xor B     '00 Or 10 
        If R <> 2 Then
            System.Console.WriteLine("#Error With XOr Operator") : Return 1
        End If
    End Function
End Module
