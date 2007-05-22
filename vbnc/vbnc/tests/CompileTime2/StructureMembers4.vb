Namespace StructureMembers4
	Class C
		Shared Function Main As Integer
			Dim s As New S(2)
			Return S.I - 2
		End Function
	End Class
	Structure S
		Public I As Integer
		Sub New(V As Integer)	
			Me.I = V
		End Sub
	End Structure
End Namespace
