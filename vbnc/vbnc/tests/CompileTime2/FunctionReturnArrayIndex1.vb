Class FunctionReturnArrayIndex1
    Shared Function Test() As Integer()
        Return New Integer() {0, 1}
    End Function
    Shared Function Main() As Integer
        Return Test()(0)
    End Function
End Class