Class ArrayFunctionReturnValue1
    Shared Function Test() As String()
        Return New String() {"0"}
    End Function
    Shared Function Main() As Integer
        Return CInt(Test(0))
    End Function
End Class