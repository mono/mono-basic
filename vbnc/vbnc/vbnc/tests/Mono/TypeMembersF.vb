'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Function fun(ByRef a As Long)
        a = a + 10
    End Function
    Function fun(ByRef a As Integer)
        a = a + 20
    End Function
    Function fun(ByRef a As Decimal)
        a = a + 30
    End Function
End Class

Module M
    Function Main() As Integer
        Dim a As Integer = 10
        Dim a1 As Long = 10
        Dim a2 As Decimal = 10
        Dim o As Object = New C()
        o.fun(a)
        o.fun(a1)
        o.fun(a2)
        If a <> 30 Then
            System.Console.WriteLine("#A1 - Latebinding not working. a = " & a) : Return 1
        End If
        If a1 <> 20 Then
            System.Console.WriteLine("#A1 - Latebinding not working. a1 = " & a) : Return 1
        End If
        If a2 <> 40 Then
            System.Console.WriteLine("#A1 - Latebinding not working. a2 = " & a) : Return 1
        End If
    End Function
End Module

