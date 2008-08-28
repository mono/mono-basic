Class GenericMethod2
    Shared Function GenericMethod(Of T)() As T
        Return Nothing
    End Function

    Shared Function Main() As Integer
        Return GenericMethod(Of String)().IndexOf("2")
    End Function
End Class