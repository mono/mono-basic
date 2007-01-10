'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module NewTest
    Structure Point
        Public x, y, A As Integer
        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Me.x = x
            Me.y = y
        End Sub

        Public Function Add() As Object
            A = Me.y + Me.x
            If A <> 700 Then
                console.writeline("Unexpected behavior:: A should be equal to Me.X + Me.Y=300+400 = 700 but got A=" & A)
                failed = True
            End If
        End Function
    End Structure
    Dim failed As Boolean
    Function Main() As Integer
        Dim R As New Point(300, 400)
        R.Add()
        If failed Then Return 1
    End Function
End Module
