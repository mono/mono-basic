Public Class SomeClass

	Public Property Prop As Integer = 57
        Get
        End Get
        Set(value As Integer)
        End Set
    End Property

    Public MustOverride Property Prop As New System.Collections.Generic.List(Of String)

End Class