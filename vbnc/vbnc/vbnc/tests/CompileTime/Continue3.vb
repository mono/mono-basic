Class Continue3
    Sub Test()
        For i As Integer = 1 To 2
            Try

            Catch ex As Exception
                Continue For
            End Try
        Next
    End Sub
End Class
