Class C
    Sub A(ByRef C As C)
    End Sub

    Sub B()
        A(rw)
        A(ro)
        A(wo)
    End Sub

    Property RW As C
        Get
            Return Nothing
        End Get
        Set(ByVal value As C)

        End Set
    End Property

    ReadOnly Property RO As C
        Get
            Return Nothing
        End Get
    End Property

    WriteOnly Property WO As C
        Set(ByVal value As C)

        End Set
    End Property
End Class