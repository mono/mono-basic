'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Public Function fun(ByVal i As Integer, ByVal a As Long)
        If i = 2 And a = 1 Then
            Return 10
        End If
        Return 11
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Integer = o.fun(a:=1, i:=2)
        If a <> 10 Then
            System.Console.WriteLine("#A1 - Binding not proper") : Return 1
        End If
    End Function
End Module
