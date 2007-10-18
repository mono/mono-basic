Class OptionalParameters1
    Shared Function Test(ByVal param1 As Integer, Optional ByVal param2 As String = "") As Integer
        Return param1
    End Function
    Shared Function Main() As Integer
        Return Test(0)
    End Function
End Class