'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Class A
    ' Has implicit Constructor defined
End Class

Class AB
    Inherits A
    Sub New()
    End Sub
End Class

Module Test
    Public Function Main() As Integer
        Dim a As AB = New AB()
    End Function
End Module

