'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Numeric Literals IntegerLiteral ::= IntegralLiteralValue [ IntegralTypeCharacter ] 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = 500.0
        If A <> 500 Then
            System.Console.WriteLine(" Unexpected Result for the Expression. A = 500 was expected  ") : Return 1
        End If
    End Function
End Module

