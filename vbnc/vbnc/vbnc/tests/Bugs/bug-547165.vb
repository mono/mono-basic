Structure S
    Dim V As Integer
    Public Shared Widening Operator CType (n As Integer) As S
        Return New S
    End Operator
End Structure
 
Class C
    Shared Sub Main
        Dim c As S
        c = 2
    End Sub
End Class