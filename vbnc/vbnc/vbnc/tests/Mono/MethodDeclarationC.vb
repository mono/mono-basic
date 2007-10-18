'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System

Module MethodDeclarationA
    Function A(ByRef i As Integer) As Integer
        i = 19
    End Function
    Function Main() As Integer
        Dim i As String
        A(i)
        If i <> "19" Then
            System.Console.WriteLine("ByRef not working") : Return 1
        End If
    End Function
End Module

