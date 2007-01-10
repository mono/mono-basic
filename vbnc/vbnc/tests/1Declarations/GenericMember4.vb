Class GenericMember4
    Class Generic(Of V)
        Public Value As Integer
        Property P1() As Integer
            Get
                Return Value
            End Get
            Set(ByVal value As Integer)
                Me.Value = value
            End Set
        End Property
    End Class

    Shared Function Main() As Integer
        Dim g As New generic(Of Integer)
        g.Value = 2
        Return g.value - 2
    End Function
End Class