Namespace Constraint2
	Class M
		Shared Function G(Of T As {Structure}) () As T
			Return New T ()
		End Function
		Shared Function Main As Integer
			Return G(Of Integer)()
		End Function
	End Class
End Namespace
