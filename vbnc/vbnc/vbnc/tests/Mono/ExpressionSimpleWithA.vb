'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'With End With


Imports System
Module NewTest
    Public Structure Point
        Dim x As Integer
        Dim y As Integer
    End Structure

    Function Main() As Integer
        Dim udtPt As POINT
        With udtPt
            .x = 10
            .y = 100
        End With
        If udtpt.x <> 10 Then
            Throw New Exception("Unexpected Behavior udtpt.x should be equal to 10 but got " & udtpt.x)
        End If
    End Function
End Module
