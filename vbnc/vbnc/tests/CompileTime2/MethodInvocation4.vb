Class MethodInvocation4

    Shared Function T(Of V)() As Integer
        Return 0
    End Function

    Shared Function T(Of S, Q)() As Integer
        Return 1
    End Function

    Shared Function Main() As Integer
        Return T(Of String)()
    End Function
End Class