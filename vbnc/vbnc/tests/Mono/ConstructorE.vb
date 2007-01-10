'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Structure A
    Dim i As Integer
    Sub New(ByVal I As Integer)
    End Sub
    Sub New(ByVal I As Integer, ByVal J As Integer)
    End Sub
    Shared Sub New()
    End Sub
End Structure

Module Test
    Public Function Main() As Integer
        Dim a As A = New A(10)
    End Function
End Module

