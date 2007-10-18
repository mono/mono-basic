'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Class C1
    Public Function fun(ByRef a As Integer) As String
        a = a Mod 2
    End Function
End Class

Class C
    Inherits C1
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Double = 1.33234
        o.fun(a)
        If a <> 1 Then
            System.Console.WriteLine("#A1 - Binding not proper") : Return 1
        End If
    End Function
End Module
