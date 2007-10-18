'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports Microsoft.Visualbasic
Imports System
Module ExpressionLiteralString
    Function Main() As Integer
        Dim A As String = "Check"
        Dim B As String = "Concat" + "enation"
        Dim C As String = "CheckConcatenation"
        Dim D As String = A + B
        If C <> D Then
            System.Console.WriteLine(" Unexpected Behavior of the Expression. A + B should reflect CheckConcatenation ") : Return 1
        End If
    End Function
End Module
