MustInherit Class MustOverride1
	MustOverride ReadOnly Property Test1() As String
End Class
Class MustOverride1_Inheriter
    Inherits MustOverride1

    Overrides ReadOnly Property Test1() As String
        Get
            Return ""
        End Get
    End Property

End Class