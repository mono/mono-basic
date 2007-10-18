Class GenericInterface2
    Class Base(Of T)
        Inherits System.Collections.Generic.List(Of T)

        Public Shadows Sub AddRange(ByVal c As System.Collections.Generic.ICollection(Of T))
        End Sub
    End Class

    Class Derived
        Inherits Base(Of GenericInterface2)

    End Class

    Sub AddVariables(ByVal list As System.Collections.Generic.ICollection(Of GenericInterface2))
        Dim m_Variables As New Derived
        m_Variables.AddRange(list)
    End Sub
End Class