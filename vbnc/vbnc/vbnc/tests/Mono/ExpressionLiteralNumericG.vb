'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module SimpleExpressionLiterals
    Function Main() As Integer
        Dim BigDec As Decimal = 9223372036854775808D
        Dim BigDoub As Double
        BigDoub = BigDec
        Dim BigNew = BigDoub
        If BigNew <> BigDec Then
            System.Console.WriteLine("Error With Given Expression. BigNew should be Equal to BigDec") : Return 1
        End If
    End Function
End Module

