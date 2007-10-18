'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System

Module MethodDeclarationA
    Function A1(ByVal ParamArray j() As Date)
        Dim i As Date
        For Each i In j
        Next i
    End Function
    Function Main() As Integer
        Dim ar As Date() = {}
        A1(ar)
    End Function
End Module

