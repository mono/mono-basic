Class ExitDoStatement1
    Shared Function Main() As Integer
        Dim i As Integer = 10
        Do While True
            i -= 1
            If i = 0 Then Exit Do
        Loop
        Return i
    End Function
End Class