'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Function fun(ByRef a As Long, ByRef a1 As Integer)
        a = a + 10
        a1 = a1 + 20
    End Function
    Function fun(ByRef a As Integer, ByRef a1 As Long)
        a = a + 20
        a1 = a1 + 10
    End Function
End Class

Module M
    Function Main() As Integer
        Dim a As Integer = 10
        Dim a1 As Long = 10
        Dim o As Object = New C()
        o.fun(a, a1)
        If a <> 30 Then
            System.Console.WriteLine("#A1 - Latebinding not working. a = " & a) : Return 1
        End If
        If a1 <> 20 Then
            System.Console.WriteLine("#A1 - Latebinding not working. a1 = " & a) : Return 1
        End If
    End Function
End Module

