'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Option Explicit On
Imports System

Class AB
    Public WriteOnly Property Prop(ByVal a As Integer) As Integer
        Set(ByVal value As Integer)
            value = 0
        End Set
    End Property
End Class

Module Test
    Public Function Main() As Integer
    End Function
End Module


