Class PropertyAccessExpression4
    Private m_Self As PropertyAccessExpression4
    Private m_Value As Integer

    ReadOnly Property Self() As PropertyAccessExpression4
        Get
            Return m_Self
        End Get
    End Property

    ReadOnly Property Value() As Integer
        Get
            Return m_Value
        End Get
    End Property

    Sub New(ByVal value As Integer)
        If value > 0 Then
            m_Self = New PropertyAccessExpression4(value - 1)
        End If
        m_Value = value
    End Sub

    Shared Function Main() As Integer
        Dim t As New PropertyAccessExpression4(2)
        Return t.Test
    End Function

    Function Test() As Integer
        Return Me.self.self.value
    End Function
End Class