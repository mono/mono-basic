'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ExpressionOperator1

    Function Main() As Integer
        Dim T As Boolean = True
        Dim F As Boolean = False
        If T > F Then
            System.Console.WriteLine("Exception occured Value of True Should be less than False ") : Return 1
        End If
    End Function
End Module




