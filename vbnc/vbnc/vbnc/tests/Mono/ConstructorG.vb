'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To check if initializing takes place according to the occurance

Imports System

Class A
    Public i As Integer = j
    Public j As Integer = 1
    Sub New()
        If i <> 0 Then
            test.result = False
        End If
    End Sub
End Class

Class AB
    Inherits A
    Public k As Integer = i
    Sub New()
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

