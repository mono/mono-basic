
Class A
    Private Sub New()

    End Sub
End Class

Class B
    Sub M(Of T As New)()
        M(Of A)()
    End Sub
End Class