Class InterfaceMemberSpecifier2
    Interface I
        ReadOnly Property P() As Boolean
    End Interface
    Class C
        Implements I

        ReadOnly Property T() As Boolean Implements I.P
            Get
                Return True
            End Get
        End Property
    End Class
End Class