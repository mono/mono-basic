Class AddHandler1
    Event a()
	Sub Test()
        AddHandler a, AddressOf Test
    End Sub
End Class
