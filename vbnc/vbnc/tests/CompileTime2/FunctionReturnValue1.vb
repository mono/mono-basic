Class FunctionReturnValue1
    Shared Function Main() As Integer
        Return Main(0)
    End Function
    Shared Function Main(ByVal Value As Integer) As Integer
        Return value
    End Function
End Class