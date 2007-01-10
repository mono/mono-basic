'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ConversionOrOperator

    Function Main() As Integer
        Dim A As Integer = 254
        Dim B As Byte = 255
        Dim R As Boolean
        R = (B < A) Or (A > B)
        If R = True Then
            System.Console.WriteLine("#Error With Or Operator") : Return 1
        End If
    End Function
End Module
