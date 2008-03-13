Class test1
    Inherits System.Web.UI.Page

    Public var As System.Web.UI.Page

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles var.Click
        Throw New Exception("YAYAY")
    End Sub
End Class