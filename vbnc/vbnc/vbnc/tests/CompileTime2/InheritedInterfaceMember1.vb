Class InheritedInterfaceMember1
    Shared Function Main() As Integer
        Dim ex As New Exception
        Return ex.Data.Count()
    End Function
End Class