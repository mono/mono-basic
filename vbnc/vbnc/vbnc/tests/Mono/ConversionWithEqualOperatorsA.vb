'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ExpressionOperator1

    Function Main() As Integer
        Dim a As Boolean = True
        Dim b As Double = 5.7
        If a = b Then
            System.Console.WriteLine("# Exceptions occured Equal To operator not working") : Return 1
        End If
    End Function
End Module

