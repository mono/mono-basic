Class TryCatch1
	Sub Test
		Try
			Dim i As Integer
		Catch ex As Exception
			Dim j As Integer
		Catch ex As exception When False
			Dim k As Integer
		Catch When True = True
			Dim l As Integer
		End Try
	End Sub
End Class
