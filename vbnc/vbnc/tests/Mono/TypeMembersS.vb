'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System

Class C
    Public a As Integer() = {1, 2, 3}
End Class

Class B
    Function fun(ByRef i() As Integer, ByVal j As Integer)
        i(j) = 0
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim o1 As Object = New B()
        o1.fun(o.a, 1)
        If o.a(1) Then
            System.Console.WriteLine("LateBinding Not Working") : Return 1
        End If
    End Function
End Module
