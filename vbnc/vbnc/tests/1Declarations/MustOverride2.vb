MustInherit Class MustOverride2
	Overridable Function Test1() As String

	End Function
	Protected Overrides Sub Finalize()

	End Sub
End Class
Class MustOverride2_Inheriter
    Inherits MustOverride2

    Function Test1() As String

    End Function
    Protected Overrides Sub Finalize()

    End Sub
End Class