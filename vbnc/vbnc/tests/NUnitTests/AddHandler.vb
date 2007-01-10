Class AddHandlerTests
    Event Event1()
    Event Event2(ByVal value As String)
    Sub Test()
        AddHandler Event1, AddressOf Me.Test
    End Sub
End Class