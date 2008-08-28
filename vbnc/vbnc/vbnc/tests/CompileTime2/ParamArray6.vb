Class ParamArray6
    Shared Function T(ByVal ParamArray P() As Object) As Integer
        Return p.length
    End Function

    Shared Function Main() As Integer
        Dim i As Integer = t(CObj(""))
        Return T("") - 1
    End Function
End Class
