Class GenericMethod1
    Shared Function GenericMethod(Of T As Class)() As Integer
        Return 1
    End Function

    Shared Function Main() As Integer
        Return GenericMethod(Of GenericMethod1)()
    End Function
End Class