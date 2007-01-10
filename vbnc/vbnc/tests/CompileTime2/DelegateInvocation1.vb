Class DelegateInvocation1
    Delegate Function Delegate1() As Integer

    Shared Function Invocation() As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Dim d As Delegate1
        d = New Delegate1(AddressOf Invocation)
        Return d()
    End Function
End Class