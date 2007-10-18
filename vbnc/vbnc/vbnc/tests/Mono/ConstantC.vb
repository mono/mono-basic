'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Checking on all possible acessors on const
Imports System

Module Test
    Const a As Integer = 1
    Public Const a1 As Integer = 1
    Private Const a2 As Integer = 1
    Class C
        Protected Const a3 As Integer = 1
    End Class
    Public Function Main() As Integer
    End Function
End Module

