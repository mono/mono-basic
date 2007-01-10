Class ResumeNext1
    Sub Test()
        Dim i As Long = 52
        On Error GoTo errh

errh:
        i = 53
        Resume Next
        i = 54
    End Sub
End Class
