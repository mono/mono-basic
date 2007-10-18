Class GenericMember5
    Inherits System.Collections.Generic.List(Of Integer)

    Function Test() As Integer
        Add(1)
        MyBase.Add(2)
        Return Me.Count - 2
    End Function

    Shared Function Main() As Integer
        Dim g As New GenericMember5
        Return g.Test
    End Function
End Class