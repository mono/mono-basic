'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off

Imports System

Module ExpressionDBNull

    Function Main() As Integer
        Dim A As String = "Something"
        Dim B As System.DBNull
        Dim C = B + A
        Dim D = "Something"
        If D <> C Then
            Throw New Exception(" Unexpected Behavior.System.DBNull should return a literal Nothing. Expected C = Something ")
        End If
    End Function
End Module
