'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module ExpressionReclassify

    Function Main() As Integer
        Dim A As Integer = 10
        Dim B As Integer = 11
        B = A
        If B <> 10 Then
            Throw New Exception(" Unexpected Behavior of the Expression. B should be reclassified as A. Expected B = 10 but got B=" & B)
        End If
    End Function
End Module
