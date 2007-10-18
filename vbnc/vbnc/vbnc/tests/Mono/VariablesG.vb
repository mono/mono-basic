'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Date and String are loosely typed.
Option Strict Off

Imports System

Module Default1
    Function Main() As Integer
        Dim a As Date
        Dim b As String
        b = a
        a = b
    End Function
End Module
