Class DefaultProperty1
    Private m_Value As String
    Default Property Test(ByVal Index As Integer) As String
        Get
            Return m_Value
        End Get
        Set(ByVal value As String)
            m_Value = value
        End Set
    End Property
End Class