Class MethodResolution3
    Shared Function Test(ByVal p1 As String, ByVal p2 As Object, ByVal p3 As Object) As Integer
        Return 0
    End Function
    Shared Function Test(ByVal p1 As String, ByVal ParamArray p As Object()) As Integer
        Return 1
    End Function
    Shared Function Main() As Integer
        Dim v As Object = ""
        Return test("", v, v)
    End Function
End Class