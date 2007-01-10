Imports system
Class UsingStatement2
    Implements IDisposable

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub
    sub New()
    end sub
    Sub New(SomeThing as String)
    End Sub

    Sub Test1()
        Using a As system.idisposable = Nothing

        End Using
    End Sub

    Sub Test2()
        Using b As New UsingStatement2("Testing")

        End Using
    End Sub
End Class