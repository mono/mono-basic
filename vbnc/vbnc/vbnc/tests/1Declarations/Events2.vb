imports system
Class Events2_a_event
    Dim M As String
    Custom Event M2 As EventHandler
        AddHandler(ByVal value As EventHandler)
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
        End RaiseEvent
    End Event
End Class
Class Events2_b_event
    Event b(ByVal bb As String)
End Class
Class Events2_c_event
    Event c(ByRef cc As String)
End Class