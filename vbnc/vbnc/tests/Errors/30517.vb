Class bug_a
	private function b as integer
	end function
	private function b(a as integer) as integer
	end function
End Class
class bug
	Shared Function Main () As Integer
		Dim c As New bug_a
		dim o As Object = c.b
	End Function
end class
