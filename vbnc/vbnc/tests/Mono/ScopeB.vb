Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To check if the Inner class is accessed or the outer class is accessed... Inner class is accessed in this case 

Class A
    Shared Function fun(ByVal i As Integer)
        System.Console.WriteLine("#A1 Outer Integer") : Return 1
    End Function
    Shared Function fun(ByVal i As String)
        System.Console.WriteLine("#A2 Outer String") : Return 1
    End Function
    Class AB
        Function gun()
            fun(1)
            fun(2)
        End Function
        Shared Function fun(ByVal i As Integer)
            'System.Console.WriteLine("Inner class Integer {0}",i)
        End Function
    End Class
End Class

Module ScopeA
    Function Main() As Integer
        Dim a As A.AB = New A.AB()
        a.gun()
    End Function
End Module
