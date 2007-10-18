'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Numeric Literals IntegerLiteral ::= IntegralLiteralValue [ IntegralTypeCharacter ] 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = 45S
        Dim B = 45I
        If A <> B Then
            System.Console.WriteLine(" Unexpected Result for the Expression. Expected was A = B = 45 ") : Return 1
        End If
    End Function
End Module
