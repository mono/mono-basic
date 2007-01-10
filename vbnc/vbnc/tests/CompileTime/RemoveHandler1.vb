Class RemoveHandler1
	Private Raiser As eventraiser
	Sub Test()
		RemoveHandler raiser.a, AddressOf handler
	End Sub
	Sub Handler()

	End Sub
End Class

Class EventRaiser
	Event a()
End Class
