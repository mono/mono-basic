'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System

Structure Somestruct
    Dim a As String
    Const b As Integer = 25
End Structure


Module M
    Function Main() As Integer
        Dim x As Somestruct

        x.a = 10
        If x.a <> 10 Then
            Throw New Exception("Expected x.a = 10 but got " & x.a)
        End If

        Dim y As Somestruct = x

        x.a = 20
        If y.a <> 10 Then
            Throw New Exception("Expected y.a = 10 but got " & y.a)
        End If
    End Function
End Module
