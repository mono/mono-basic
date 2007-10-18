'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System
Module ExpressionOperator1

    Function Main() As Integer
        Dim SomeDate1 As Date = #2/10/2005 11:11:00 PM#
        Dim SomeDate2 As Date = #2/11/2005 12:12:00 PM#
        If SomeDate1 >= SomeDate2 Then
            System.Console.WriteLine("# Error D1 can't be greater than D2 ") : Return 1
        End If
    End Function
End Module
