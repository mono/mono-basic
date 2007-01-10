Class PropertyAccess5
    Private m_Test As Integer
    Property Test() As Integer
        Get
            Return m_Test
        End Get
        Set(ByVal value As Integer)
            m_Test = value
        End Set
    End Property

    Shared Function Main() As Integer
        Dim t As New propertyaccess5
        t.test = 5
        Return t.test - 5
    End Function
End Class