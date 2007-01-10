'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Imports A

Namespace A
    Public Class B
        Public Shared i As Integer
    End Class
End Namespace

Class AB
    Shared Sub New()
        A.B.i = A.B.i + 1
    End Sub
End Class

Module Test
    Public Function Main() As Integer
        Dim a1 As AB = New AB()
        Dim b2 As AB = New AB()
        Dim c3 As AB = New AB()
        If A.B.i <> 1 Then
            System.Console.WriteLine("Shared Constructor not working") : Return 1
        End If
    End Function
End Module

