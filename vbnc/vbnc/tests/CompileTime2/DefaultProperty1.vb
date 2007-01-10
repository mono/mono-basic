Class DefaultProperty1
    Private m_Value As Integer
    Default Property Item(ByVal Index As Integer) As Integer
        Get
            Return m_value
        End Get
        Set(ByVal value As Integer)
            m_value = value
        End Set
    End Property

    Shared Function Main() As Integer
        Dim d As New DefaultProperty1
        d(0) = 1
        Return d(1) - 1
    End Function
End Class