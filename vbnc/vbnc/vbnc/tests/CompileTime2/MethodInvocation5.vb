Imports System.Reflection

Class MethodInvocation5
    Shared Function IsAccessible(ByVal FieldAccessability As FieldAttributes, ByVal CalledType As Type, ByVal CallerType As Type) As Integer
        Return 1
    End Function

    Shared Function IsAccessible(ByVal CalledMethodAccessability As MethodAttributes, ByVal CalledType As Type, ByVal CallerType As Type) As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Return IsAccessible(MethodAttributes.Public, Nothing, Nothing)
    End Function
End Class