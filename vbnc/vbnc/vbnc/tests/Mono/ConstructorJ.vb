'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Checking circular references

Imports System

Class A
    Public Shared X As Integer = B.Y + 1
    Shared Function Hello() As Object
        If A.X <> 2 Or B.Y <> 1 Then
            System.Console.WriteLine("Shared Construtor not working") : Return 1
        End If
    End Function
End Class

Class B
    Public Shared Y As Integer = A.X + 1
End Class


Module Test
    Public Function Main() As Integer
        A.Hello()
    End Function
End Module

