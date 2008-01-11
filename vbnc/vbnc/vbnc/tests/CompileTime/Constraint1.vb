Namespace Constraint1
	Class A (Of Z As Class)
	End Class
	Class B (Of Z As Structure)
	End Class
	Class Q
		Dim A As A(Of Object)
		Dim B As B(Of Integer)
	End Class
End Namespace
