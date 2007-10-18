'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Numeric Literals IntegerLiteral ::= IntegralLiteralValue [ IntegralTypeCharacter ] 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = &H8000S
        If A <> -32768 Then
            Throw New Exception(" Unexpected Result for the Expression. Value of A should be -32768 but got " & A)
        End If
    End Function
End Module

