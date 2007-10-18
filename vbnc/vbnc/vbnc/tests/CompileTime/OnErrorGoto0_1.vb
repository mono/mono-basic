Class OnErrorGoto0_1
    Sub Test()
        Dim i As Integer = 120
        Dim k As Integer = 130
        On Error GoTo Label
        i = 26
        On Error GoTo 0
        i = 25
        On Error GoTo Label
        i = 24
Label:

    End Sub
End Class
