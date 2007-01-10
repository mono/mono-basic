Class MyBaseExpression1
	Sub test()
		Dim str As String
		str = MyBase.tostring()
		str = Me.Tostring()
	End Sub
	overrides function ToString() as String
		return ""
	end function
End Class