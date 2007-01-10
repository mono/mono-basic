Class Optional1
	Enum test1
		value = 5
	End Enum
	Sub op1(Optional ByVal value As String = "")

	End Sub

	Function op2(Optional ByVal value As Integer = test1.value) As Long

	End Function
End Class