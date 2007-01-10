'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Class C1
    Public Function fun() As String
        Return fun
    End Function
End Class
Class C
    Inherits C1
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Integer = o.fun()
        If a <> 0 Then
            System.Console.WriteLine("#A1 - Binding not proper") : Return 1
        End If
    End Function
End Module
