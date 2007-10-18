'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ExpressionOperator1

    Function Main() As Integer
        Dim a As Short = 5.5
        Dim b As Integer = 6
        a += 1
        If a = b Then
            System.Console.WriteLine("# = operators: Failed") : Return 1
        End If
    End Function
End Module


