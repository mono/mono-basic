'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Function fun(ByRef a As Long)
        a = a + 20
    End Function
    Function fun(ByRef a As Integer)
        a = a + 10
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Integer = 10
        o.fun(a)
        If a <> 20 Then
            System.Console.WriteLine("#A1 - Binding not working") : Return 1
        End If
    End Function
End Module
