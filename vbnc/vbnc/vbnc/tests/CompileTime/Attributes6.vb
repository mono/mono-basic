Class Attributes6
    <Attributes6("Property")> _
    Property P() As Integer
        Get

        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    <Attributes6("Sub")> _
    Sub S()

    End Sub

    <Attributes6("Function")> _
    Function F() As Object

    End Function
End Class
Class Attributes6Attribute
    Inherits System.Attribute

    Sub New(ByVal Description As String)
    End Sub
End Class
