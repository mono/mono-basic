Imports system
Imports System.Diagnostics
Class UsingStatement4
    Implements System.IDisposable
    Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
    Sub Test()
        Using x As New UsingStatement4
        End Using
    End Sub
End Class