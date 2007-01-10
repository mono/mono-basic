Class RedimPreserve2
	Sub Test()
		dim preserve as integer
		Dim i As Integer()
		Dim j As Integer()
		ReDim Preserve i(2)
		ReDim Preserve i(5), j(4)
	End Sub
End Class
