Class ByRefLiteralConversion1
    Shared I As Integer
    Shared Sub T(ByRef D As Double)
        I = 2
    End Sub
    Shared Function Main() As Integer
        T(2)
        Return i - 2
    End Function
End Class