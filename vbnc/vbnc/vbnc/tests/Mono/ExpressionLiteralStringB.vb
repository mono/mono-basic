'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports Microsoft.VisualBasic
Imports System
Module ExpressionLiteralString
    Function Main() As Integer
        Dim A = """"
        Dim B As Char = chr(34)
        If B <> A Then
            System.Console.WriteLine(" Unexpected Result for the Expression ") : Return 1
        End If
    End Function
End Module
