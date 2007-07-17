Class EventMember3
    Event SomeEvent()
End Class

Class EventMember3_Derived
    Inherits EventMember3

    Sub EventHandler() Handles mybase.someevent

    End Sub
End Class