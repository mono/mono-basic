Class Throw1
    Sub test()
        Try

        Catch ex As Exception
            Throw
            Throw New system.exception
        End Try
    End Sub
End Class