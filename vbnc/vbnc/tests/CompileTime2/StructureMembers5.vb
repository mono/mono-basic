Namespace StructureMembers5
	Class C
		Shared Function Main As Integer
			Dim s As New S(2)
            Return S.F - 2
		End Function
	End Class
	Structure S
		Public I As Integer
		Sub New(V As Integer)	
            I = V
        End Sub
        Function F() As Integer
            Return Me.P
        End Function
		ReadOnly Property P As Integer
			Get
				Return I
			End Get
		End Property
	End Structure
End Namespace
