'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off

Imports System
Module ExpressionStringNothing

    Function Main() As Integer
        Dim A As String = "Something"
        Dim B As String = Nothing
        Dim C = A + B
        Dim D = "Something"
        If D <> C Then
            Throw New Exception(" Unexpected Behavior.Nothing is treated as if it were the empty string literal "" and D should be euqal to A+B")
        End If
    End Function
End Module


