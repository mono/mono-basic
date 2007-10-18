Class GenericDelegateTypeParameter1
    Delegate Sub Method1(Of T)()
    Delegate Function Method2(Of T)() As T
    Delegate Sub Method3(Of T)(ByVal Param As T)

    Shared Function Main() As Integer

    End Function
End Class