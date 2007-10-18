Class OnErrorTest
	Sub a()
		On Error GoTo 0
	End Sub
	Sub b()
		On Error GoTo -1
	End Sub
End Class