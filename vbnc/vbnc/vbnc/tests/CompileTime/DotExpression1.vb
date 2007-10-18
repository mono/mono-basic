Class DotExpression1
	Sub test()
		Dim o As Object
		Dim str As String
		Dim i As Integer
		With o
			str = .ToString()
			i = System.String.Compare(.ToString, .ToString)
		End With
	End Sub
End Class