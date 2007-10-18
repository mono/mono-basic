Class WithEvents1
	Class EventRaiser
        Public Shared Event E()
	End Class
    WithEvents W As EventRaiser
    Private Sub Handler() Handles W.E

    End Sub
End Class