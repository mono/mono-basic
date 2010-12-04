Class C
    Public value As Integer
    Sub New(ByVal v As Integer)
        value = v
    End Sub

End Class

Module ByRefProperty1
    Dim m_rw As Integer = 1
    Dim m_ro As Integer = 10
    Dim m_rw_c As New c(1)
    Dim m_ro_c As New c(10)

    Sub A(ByRef i As Integer)
        i *= 2
    End Sub

    Sub B(ByRef cc As C)
        cc = New c(0)
    End Sub

    Public Function Main() As Integer
        Dim local_rw_c As c = m_rw_c
        Dim local_ro_c As c = m_ro_c

        A(rw)
        A(ro)
        If m_rw <> 2 Then
            console.writeline("RW property failed, got value {0} expected 2", m_rw)
            Return 1
        End If
        If m_ro <> 10 Then
            console.writeline("RO property failed, got value {0} expected 10", m_ro)
            Return 2
        End If

        B(rw_c)
        B(ro_c)
        If m_rw_c Is local_rw_c Then
            console.writeline("RW_C property failed")
            Return 3
        End If
        If m_ro_c Is local_ro_c = False Then
            console.writeline("RO_C property failed")
            Return 4
        End If

        Return 0
    End Function

    Property RW_C As C
        Get
            Return m_rw_c
        End Get
        Set(ByVal value As C)
            m_rw_c = value
        End Set
    End Property

    ReadOnly Property RO_C As C
        Get
            Return m_ro_c
        End Get
    End Property

    Property RW As Integer
        Get
            Return m_rw
        End Get
        Set(ByVal value As Integer)
            m_rw = value
        End Set
    End Property

    ReadOnly Property RO As Integer
        Get
            Return m_ro
        End Get
    End Property
End Module