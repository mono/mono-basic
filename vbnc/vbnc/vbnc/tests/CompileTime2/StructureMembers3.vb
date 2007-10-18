Namespace StructureMembers3
	Class C
		Public S As S
		Shared Function Main As Integer
			Dim s As S
			S.C = New C ()
			S.C.S.I = 2
			Return S.C.S.I - 2
		End Function
	End Class
	Structure S
		Public C As C
		Public I As Integer
	End Structure
End Namespace
