'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C1
    Public fun As Integer = 10
End Class

Class C
    Public fun As Integer = 20
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        If o.fun <> 20 Then
            System.Console.WriteLine("#A1 - Binding not proper") : Return 1
        End If
        o = New C1()
        If o.fun <> 10 Then
            System.Console.WriteLine("#A2 - Binding not proper") : Return 1
        End If
    End Function
End Module
