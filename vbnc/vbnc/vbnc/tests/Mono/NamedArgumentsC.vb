Option Strict Off
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Class C
    Public Function fun(ByVal i As Integer, Optional ByVal a1 As Char = "d", Optional ByVal j As Integer = 30) As Integer
        If a1 = "c" And i = 2 And j = 30 Then
            Return 10
        End If
        Return 11
    End Function
End Class

Module M
    Public i As Integer
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Integer = o.fun(a1:="caa", i:=2.321)
        If a <> 10 Or i = 2 Then
            System.Console.WriteLine("#A1 - Binding not proper") : Return 1
        End If
    End Function
End Module
