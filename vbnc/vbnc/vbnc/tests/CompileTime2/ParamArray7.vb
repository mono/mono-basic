Class ParamArray7
    Private Function SaveMessage(ByVal Msg As String) As Integer
        Return 1
    End Function

    Private Shared Function SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As String) As Integer
        Return 2
    End Function

    Private Shared Function SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As Object) As Integer
        Return 0
    End Function

    Public Shared Function Main() As Integer
        Return SaveMessage("a", New ParamArray7())
    End Function
End Class
