'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System

Class C
    Public a As Integer() = {1, 2, 3}
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        o.a(1) = 0
        If o.a(1) Then
            System.Console.WriteLine("LateBinding Not Working") : Return 1
        End If
    End Function
End Module
