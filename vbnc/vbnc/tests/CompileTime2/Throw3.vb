Module Throw3

    Function Main() As Integer
        Try
            Throw New Exception("Exception!")
            Return 1
        Catch ex As exception
            Return 0
        End Try
    End Function

End Module
