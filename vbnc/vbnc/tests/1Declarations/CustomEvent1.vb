Class CustomEvent1
    Delegate Sub a()
    Custom Event aa As a
        AddHandler(ByVal value As a)

        End AddHandler

        RemoveHandler(ByVal value As a)

        End RemoveHandler

        RaiseEvent()

        End RaiseEvent
    End Event
End Class

