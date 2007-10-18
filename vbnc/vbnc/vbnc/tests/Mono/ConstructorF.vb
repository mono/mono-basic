'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Class A
    Public i As Integer = 10
    Sub New()
    End Sub
End Class

Class AB
    Inherits A
    Public i1 As Integer = MyBase.i
    Sub New()
        If i1 <> 10 Then
            test.result = False
        End If
    End Sub
End Class

Module Test
    Public result As Boolean = True
    Public Function Main() As Integer
        Dim a As AB = New AB()
        If result = False Then
            System.Console.WriteLine("Constructor not working properly") : Return 1
        End If
    End Function
End Module

