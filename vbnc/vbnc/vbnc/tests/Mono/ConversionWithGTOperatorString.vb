'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ExpressionOperatorStrings

    Function Main() As Integer
        Dim Str1 As String = "Test1"
        Dim Str2 As String = "Test2"
        If Str1 > Str2 Then
            System.Console.WriteLine("Exception occured Str1 can't be greater that Str2") : Return 1
        End If
    End Function
End Module

