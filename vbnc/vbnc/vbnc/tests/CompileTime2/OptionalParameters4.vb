Class OptionalParameters4

    Shared Function Test(ByVal Param1 As Integer, Optional ByVal Param2 As OptionalParameters4 = Nothing, Optional ByVal Param3 As Integer = 0) As Integer
        Return 0
    End Function

    Shared Function Test(ByVal Param1 As Integer, ByVal Param2 As OptionalParameters4, ByVal Param3 As Long) As Integer
        Return 1
    End Function

    Shared Function Main() As Integer
        Return Test(0, Nothing, 0)
    End Function
End Class