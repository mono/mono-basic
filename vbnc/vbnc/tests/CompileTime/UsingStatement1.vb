Imports system
Class UsingStatement1
    Implements IDisposable

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Sub Test1()
        Using a As system.idisposable = Nothing

        End Using
    End Sub

    Sub Test2()
        Using b As New UsingStatement1

        End Using
    End Sub
End Class