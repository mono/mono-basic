Class GenericMember4
    Class Generic(Of V)
        Public Value As Integer
        Public T As V
    End Class

    Sub M()
        Dim g As generic(Of Integer)
        g.Value = 2
        g.T = 2
    End Sub
End Class