MustInherit Class MultiOverride1
	MustOverride Sub a()
End Class
MustInherit Class MultiOverrideB
	Inherits MultiOverride1
End Class
Class MutliOverrideC
	Inherits MultiOverrideB
	Overrides Sub a()

	End Sub
End Class