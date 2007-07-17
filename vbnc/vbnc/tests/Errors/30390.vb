Class bug_a
	private b as integer
End Class
class bug
	Shared Function Main () As Integer
		Dim c As New bug_a
		dim o As Object = c.b
	End Function
end class
