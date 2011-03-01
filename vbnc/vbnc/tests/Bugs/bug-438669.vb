Class C
    Shared Sub Main()
    End Sub
    ReadOnly Property P(Optional ByVal arg As Integer = -1)
        Get
        End Get
    End Property
    Property Q(Optional ByVal arg As Integer = -1)
        Get
        End Get
        Set(ByVal value)

        End Set
    End Property
    WriteOnly Property R(Optional ByVal arg As Integer = -1)
        Set(ByVal value)
        End Set
    End Property

End Class