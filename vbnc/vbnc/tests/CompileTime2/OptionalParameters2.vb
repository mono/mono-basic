Class OptionalParameters2
    Shared Function Test(ByVal Param1 As Integer, Optional ByVal Param2 As OptionalParameters2 = Nothing) As Integer
        Return 0
    End Function

    Shared Function Test(ByVal Param1 As Integer()) As Integer
        Return 1
    End Function

    Shared Function Main() As Integer
        Return Test(0)
    End Function
End Class