Class EventMember7

    Event SomeEvent()
    Sub New()
        Dim i As Integer
        i = 2
    End Sub

    Sub New(ByVal a As Boolean)
        Dim i As Integer
        i = 4
    End Sub

    Sub New(ByVal B As String)
        Me.new()
    End Sub

    Sub EventHandler() Handles me.someevent

    End Sub

End Class