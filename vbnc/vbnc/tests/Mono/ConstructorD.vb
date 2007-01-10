'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To Prove Constructors can be overloaded

Imports System

Class A
    Sub New()
    End Sub
    Sub New(ByVal I As Integer)
    End Sub
    Sub New(ByVal I As Integer, ByVal J As Integer)
    End Sub
    Shared Sub New()
    End Sub
End Class

Module Test
    Public Function Main() As Integer
        Dim a As A = New A(10)
    End Function
End Module

