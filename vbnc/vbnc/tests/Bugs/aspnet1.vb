Class aspnet1
	Shared Function Main As Integer
		Dim i As Integer
		Select Case i
			Case -1:
				Return -1
			Case 0:
				Return 0
			Case 1:
				Return 1
		End Select
		Return 2
	End Function
End Class
