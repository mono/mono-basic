Class IndexedProperty1
    Private m_Value As Integer
    Property Item(ByVal Index As Integer) As Integer
        Get
            Return m_value
        End Get
        Set(ByVal value As Integer)
            m_value = value
        End Set
    End Property

    Shared Function Main() As Integer
        Dim c As New IndexedProperty1
        c.Item(0) = 2
        Return c.Item(1) - 2
    End Function
End Class