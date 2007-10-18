'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To Check if derived class need not implement the already existing Implementation

Interface A
    Function fun(ByVal a As Integer) As Object
End Interface

Interface AB
    Inherits A
    Function fun1(ByVal a As Integer) As Object
End Interface

Class B
    Implements A
    Function Cfun(ByVal a As Integer) As Object Implements A.fun
    End Function
End Class

Class BC
    Inherits B
    Implements AB
    Function Cfun1(ByVal a As Integer) As Object Implements AB.fun1
    End Function
End Class

Module InterfaceI
    Function Main() As Integer
    End Function
End Module
