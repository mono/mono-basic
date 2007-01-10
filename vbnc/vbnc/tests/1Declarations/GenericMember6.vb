Class GenericMember6
    Class Base(Of T)
        Inherits System.Collections.Generic.List(Of T)
    End Class
    Class Derived
        Inherits Base(Of Integer)
    End Class

    Shared Function Main() As Integer
        Dim g As New Derived

        g.Add(1)
        g.Add(2)

        Return g.Count - 2
    End Function
End Class