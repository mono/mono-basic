'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To Check if only those parameters that follow Optional must be Optional

Imports System

Module MethodDeclarationA
    Sub A1(ByVal i As Integer, Optional ByVal j As Integer = 10)
    End Sub
    Function Main() As Integer
    End Function
End Module

