'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Since there is only one applicable method, no late binding is involved
Option Strict Off

Imports System
Module M
    Function F(ByVal a As String) As Integer
        Return 1
    End Function
    Function Main() As Integer
        Dim obj As Object = "ABC"
        If F(obj) <> 1 Then
            System.Console.WriteLine("Overload Resolution failed in latebinding") : Return 1
        End If
    End Function
End Module
