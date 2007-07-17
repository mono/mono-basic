Class EventMember2
    Event SomeEvent()
End Class

Class EventMember2_Derived
    Inherits EventMember2

    Sub EventHandler() Handles me.someevent

    End Sub
End Class