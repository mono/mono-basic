Option Strict On
Class C1
    Sub G(ByVal i As Integer, ByVal ParamArray args() As String)
        G(, "2", "3", i:=2)
    End Sub
End Class
