'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module InterfaceA
    Interface A
        Function fun() As Object
        Function fun1() As Object
        Function fun2() As Object
        Function fun3() As Object
    End Interface

    Class B
        Implements A
        Public Function AA1() As Object Implements A.fun
        End Function
        Private Function AA2() As Object Implements A.fun1
        End Function
        Protected Function AA3() As Object Implements A.fun2
        End Function
        Function AA4() As Object Implements A.fun3
        End Function
    End Class

    Function Main() As Integer
    End Function
End Module

