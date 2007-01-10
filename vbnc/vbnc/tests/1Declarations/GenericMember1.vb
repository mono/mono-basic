Class GenericMember1
    Class Generic(Of V)
        Function F1() As Integer
            Return 1
        End Function
    End Class

    Shared Function Main() As Integer
        Dim g As New generic(Of Integer)
        Return g.F1
    End Function
End Class