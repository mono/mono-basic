'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports Microsoft.Visualbasic
Imports System
Module ExpressionLiteralString
    Function Main() As Integer
        Dim A As Object = "Test"
        Dim B As Object = "Test"
        If B <> A Then
            System.Console.WriteLine("Unexpected Behavior. B should be Equal to A as string literals refer to the same string instance ") : Return 1
        End If
    End Function
End Module
