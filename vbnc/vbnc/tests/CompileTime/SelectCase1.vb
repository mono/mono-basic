Class SelectCase1
	Sub Test
		Dim i As Integer
		Select Case i
			Case 1
				Dim j As Integer
			Case 2 To 3
				Dim j As Integer
			Case 4, 5
				Dim j As Integer
			Case 6, 7 To 8, 9
				Dim j As Integer
			Case Is > 10, Is < 11, Is = 12, Is >= 13, Is <= 14
				Dim k As Integer
			Case Else
				Dim j As Integer
		End Select
	End Sub
End Class
