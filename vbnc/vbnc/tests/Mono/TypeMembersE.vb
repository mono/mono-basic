'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    Function fun(ByRef a As Long)
        System.Console.WriteLine("#A1 - Binding not working") : Return 1
    End Function
    Function fun(ByRef a As Integer)
        a = a + 10
        If a <> 20 Then
            System.Console.WriteLine("#A1 - Binding not working") : Return 1
        End If
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        o.fun(10)   'Constant value passed            		
    End Function
End Module

