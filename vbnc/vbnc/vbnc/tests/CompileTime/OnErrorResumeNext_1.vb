Class OnErrorResumeNext_1
	Sub Test()
        Dim i As Integer = 200
        i = 50
        On Error GoTo 0
        i = 51
        On Error Resume Next
        i = 52
        On Error GoTo 0
        i = 53
    End Sub
End Class
