Class WithEvents1
	Class EventRaiser
        Public Event E()
	End Class
    WithEvents W As EventRaiser
    Private Sub Handler() Handles W.E

    End Sub
End Class