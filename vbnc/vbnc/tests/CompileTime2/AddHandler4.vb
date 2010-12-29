Class MdiParent1
    Inherits System.Windows.Forms.Form

    Public Event MailUpdateStarted()
End Class

Class Whatever
    Sub Dummy()
        AddHandler MdiParent1.MailUpdateStarted, AddressOf MailUpdateStarted
    End Sub

    Public Sub MailUpdateStarted()
    End Sub
End Class