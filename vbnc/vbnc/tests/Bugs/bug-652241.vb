
Class Class1

    Private WithEvents Timer1 As System.Timers.Timer

    Public Sub Main()
    End Sub

    Private Sub Timer1Event( _
               ByVal sender As System.Object, _
               ByVal eventArgs As System.EventArgs) _
               Handles Timer1.Elapsed
        Timer1.Stop()
20:     Exit Sub

    End Sub
End Class