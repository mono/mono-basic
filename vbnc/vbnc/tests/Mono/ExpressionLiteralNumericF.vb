'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
' Checking F and R 
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim A = 1.401298E-45F
        Dim B = 1.0R
        B = A
        Dim C = B
        If C <> A Then
            System.Console.WriteLine("Error With Expression. C should be Equal to B = A") : Return 1
        End If
    End Function
End Module
