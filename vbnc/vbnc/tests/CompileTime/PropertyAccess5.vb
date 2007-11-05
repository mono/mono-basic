Option Strict Off

Public Class A

    Public Property Attendee() As Integer
        Get
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Private Sub Method()
        Dim Members As DataView = Nothing
        Attendee = Members(0)("memberid")
    End Sub

End Class
