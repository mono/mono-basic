'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To Check if Multiple Methods can access mulitplte events

Imports System

Class Raiser
    Public Event Fun(ByVal i As Integer)
    Public Event Fun1(ByVal i As Integer)
    Public Sub New(ByVal i As Integer)
        RaiseEvent Fun(23)
    End Sub
End Class

Module Test
    Private WithEvents x As Raiser

    Private Sub Fun(ByVal i As Integer) Handles x.Fun, X.Fun1
    End Sub

    Public Sub Fun1(ByVal i As Integer) Handles X.Fun1
    End Sub


    Public Function Main() As Integer
        x = New Raiser(10)
    End Function
End Module
