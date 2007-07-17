Namespace StructureMembers7
    Class C
        Shared Function Main() As Integer
            Dim s As S
            F(s)
            Return S.I - 2
        End Function
        Shared Sub F(ByRef S As S)
            Dim s2 As S
            s = s2
            s.i = 2
        End Sub
    End Class
    Structure S
        Public I As Integer
    End Structure
End Namespace
