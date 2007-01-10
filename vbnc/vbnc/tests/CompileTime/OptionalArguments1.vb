Class OptionalArguments1
    Sub Test()
        invoked(, 2, , 4)
    End Sub
    Sub Invoked(Optional ByVal a1 As Integer = 0, Optional ByVal a2 As Integer = 1, Optional ByVal a3 As Integer = 2, Optional ByVal a4 As Integer = 3)

    End Sub
End Class