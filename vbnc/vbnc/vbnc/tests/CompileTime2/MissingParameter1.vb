Class MissingParameter1
    Shared Function Test(Optional ByVal Param As Object = Nothing) As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Return Test()
    End Function
End Class