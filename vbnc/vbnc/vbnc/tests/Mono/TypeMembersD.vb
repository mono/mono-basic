'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Function fun(ByRef a As Long)
        a = a + 10
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Const a As Integer = 10
        o.fun(a)
        If a <> 10 Then
            System.Console.WriteLine("#A1 - ByRef not working") : Return 1
        End If
    End Function
End Module
