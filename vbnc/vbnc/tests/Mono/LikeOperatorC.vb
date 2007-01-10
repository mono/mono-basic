'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Option Compare text
Imports System

Module LikeOperator2
    Public Function Main() As Integer
        Dim a As Boolean
        a = "" Like "[]"
        If a <> True Then
            System.Console.WriteLine("#A1-LikeOperator:Failed") : Return 1
        End If
    End Function
End Module
