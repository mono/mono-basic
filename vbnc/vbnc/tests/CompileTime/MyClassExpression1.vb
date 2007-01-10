Class MyClassExpression
	Sub Test()
		Dim str As String
		str = MyClass.Tostring()
	End Sub
	Overrides Function ToString() As String
		Return ""
	End Function
End Class