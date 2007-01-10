'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Numeric Literals IntegerLiteral ::= IntegralLiteralValue [ IntegralTypeCharacter ] 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = &O7
        If A <> 7 Then
            Throw New Exception(" Unexpected Result. A= 7 was expected but got A= " & A)
        End If
    End Function
End Module

