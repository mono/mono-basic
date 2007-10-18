Class Delegate1
	Sub S
		Dim o as System.CrossAppDomainDelegate
		o = New System.CrossAppDomainDelegate (AddressOf S, AddressOf S)
	End Sub
End Class

