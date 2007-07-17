Class EventMember4
    Event SomeEvent()
End Class

Class EventMember4_Handler

    Private WithEvents var As eventmember4

    Sub EventHandler() Handles var.someevent

    End Sub
End Class