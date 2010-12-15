Class Attributes8
    Class AttribAttribute
        Inherits System.Attribute
        Sub New(ByVal s As String)

        End Sub
    End Class

    Delegate Sub D()

    <Attrib("Regular Event")> _
    Event A()

    <Attrib("Custom Event")> _
    Custom Event B As D
        <Attrib("AddHandler")> _
        AddHandler(ByVal value As D)

        End AddHandler

        <Attrib("RemoveHandler")> _
        RemoveHandler(ByVal value As D)

        End RemoveHandler

        <Attrib("RaiseEvent")> _
        RaiseEvent()

        End RaiseEvent
    End Event

End Class