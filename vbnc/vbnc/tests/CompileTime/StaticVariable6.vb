Class T
    Sub New(ByVal foo As Object)

    End Sub

    Public ReadOnly Property P As Object
        Get
            Static res As New T("foo")
        End Get
    End Property
End Class
