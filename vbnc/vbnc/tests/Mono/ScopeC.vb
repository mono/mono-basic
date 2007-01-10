Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Cheks if the outer class members can be accessed using an object reference or not... of course works...
Class A
    Function fun(ByVal i As Integer)
        If i <> 1 Then
            System.Console.WriteLine("#A1 Outer Integer") : Return 1
        End If
    End Function
    Function fun(ByVal i As String)
        If i <> "Hello" Then
            System.Console.WriteLine("#A2 Outer String") : Return 1
        End If
    End Function
    Class AB
        Function gun()
            Dim a As A = New A()
            a.fun(1)
            a.fun("Hello")
        End Function
    End Class
End Class

Module ScopeA
    Function Main() As Integer
        Dim a As A.AB = New A.AB()
        a.gun()
    End Function
End Module
