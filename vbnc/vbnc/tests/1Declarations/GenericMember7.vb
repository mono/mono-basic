Class GenericMember7
    Shared Function G(Of T)() As Integer
        Return 1
    End Function

    Shared Function Main() As Integer
        Return g(Of Integer)() - 1
    End Function
End Class