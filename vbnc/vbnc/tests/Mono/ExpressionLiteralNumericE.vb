'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Numeric Literals IntegerLiteral ::= IntegralLiteralValue [ IntegralTypeCharacter ] 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = 922337203685477L
        Dim B As Long
        B = A
        Dim C As Long = B
        If C <> A Then
            System.Console.WriteLine(" Unexpected Behavior of the Expression. C should be equal to B = A  ") : Return 1
        End If
    End Function
End Module

