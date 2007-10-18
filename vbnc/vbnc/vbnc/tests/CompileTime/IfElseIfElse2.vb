Class IfElseIfElse2
	Sub Test()
		Dim i As Integer
		If True Then : i = 2
		ElseIf False Then : i = 3
		ElseIf True Then : i = 4
		Else : i = 5
		End If
	End Sub
End Class
