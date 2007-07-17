Class EventMember6
    Implements System.IDisposable

    Event SomeEvent()
    Sub New()
        Dim i As Integer
        i = 2
    End Sub
    Sub New(ByVal a As Boolean)
        Dim i As Integer
        i = 4
    End Sub
    Sub EventHandler() Handles me.someevent

    End Sub

    Sub Dispose() Implements System.IDisposable.Dispose

    End Sub

    Protected Overrides Sub Finalize()

    End Sub
End Class