Class ByRef1
    Shared Function Main() As Integer
        Dim b As Boolean
        b = Boolean.TryParse("Boolean", b)
        If b Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class