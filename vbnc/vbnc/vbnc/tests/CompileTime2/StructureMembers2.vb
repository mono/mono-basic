Namespace StructureMembers2
	Class C
		Shared Function Main As Integer
			Dim s As S
			S.A = 2
			Return S.A - 2
		End Function
	End Class
	Structure S
		Public A As Integer
	End Structure
End Namespace
