Class GenericDefaultProperty1
    Inherits System.Collections.Generic.List(Of Integer)

    Shared Function Main() As Integer
        Dim g As New GenericDefaultProperty1
        g.Add(1)
        Return g(0) - 1
    End Function
End Class