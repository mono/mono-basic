Class CustomEvent2
    Delegate Sub a(ByVal param1 As String, ByRef param2 As Integer)
    Custom Event aa As a
        AddHandler(ByVal val As a)

        End AddHandler
        RemoveHandler(ByVal val As a)

        End RemoveHandler
        RaiseEvent(ByVal param As String, ByRef param2 As Integer)

        End RaiseEvent
    End Event
End Class
