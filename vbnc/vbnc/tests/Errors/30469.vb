Class MdiParent1

    Public Event MailUpdateStarted()
End Class

Class Whatever
    Sub Dummy()
        AddHandler MdiParent1.MailUpdateStarted, AddressOf MailUpdateStarted
    End Sub

    Public Sub MailUpdateStarted()
    End Sub
End Class