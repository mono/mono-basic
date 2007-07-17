Class EventMember8
    Event SomeEvent()
End Class

Class EventMember8_Handler

    Public WithEvents var As eventmember8

    Public Sub EventHandler() Handles var.someevent

    End Sub
End Class