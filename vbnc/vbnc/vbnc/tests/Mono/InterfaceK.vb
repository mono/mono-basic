'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To Check if MustOverridable methods can Implement the interface functions

Interface A
    Function fun(ByVal a As Integer) As Object
    Function bun(ByVal a As Integer) As Object
End Interface

MustInherit Class B
    Implements A
    Function Cfun(ByVal a As Integer) As Object Implements A.fun
    End Function
    MustOverride Function Cbun(ByVal a As Integer) As Object Implements A.bun
End Class

Class C
    Inherits B
    Overrides Function Cbun(ByVal a As Integer) As Object
    End Function
End Class

Module InterfaceI
    Function Main() As Integer
    End Function
End Module
