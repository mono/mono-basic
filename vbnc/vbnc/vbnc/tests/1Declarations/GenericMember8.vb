Class GenericMember8
    Class Test
        Inherits System.Collections.Generic.List(Of Integer)

        Shadows Sub Add(ByVal i As Integer)
            MyBase.Add(i)
        End Sub
    End Class

    Shared Function Main() As Integer
        Dim g As New test
        g.add(2)
        Return g.Count - 1
    End Function
End Class