Class InterfaceMemberSpecifier1
    Interface I
        ReadOnly Property P() As Boolean
    End Interface
    Class C
        Implements I

        ReadOnly Property P() As Boolean Implements I.P
            Get
                Return True
            End Get
        End Property
    End Class
End Class