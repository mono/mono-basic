Class AddressOfExpression2
	Sub Test(ByVal ar As System.IAsyncResult)
		Dim tmp As System.AsyncCallback
		tmp = AddressOf Me.Test
	End Sub
End Class