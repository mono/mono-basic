Class PropertyAccess2

    Shared ReadOnly Property Test() As Integer()
        Get
            Return New Integer() {0}
        End Get
    End Property

    Shared Function Main() As Integer
        Return Test(0)
    End Function
End Class