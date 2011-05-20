Module ArgumentTypeInference
    Sub a(Of T)(ByVal b As Integer, ByVal bc As Integer)
        a(1)
    End Sub
    Sub a(Of T)(ByVal b As Integer, ByVal bc As Integer, ByVal bcc As Integer)

    End Sub
End Module