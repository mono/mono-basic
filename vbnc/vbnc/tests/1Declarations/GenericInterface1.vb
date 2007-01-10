Class GenericInterface1
    Private m_Variables As New System.Collections.Generic.List(Of GenericInterface1)()
    Sub AddVariables(ByVal list As System.Collections.Generic.ICollection(Of GenericInterface1))
        m_Variables.AddRange(list)
    End Sub
End Class