Friend NotInheritable Class ThreadSafeObjectProvider(Of T As New)
    Sub A
        Dim tmp As T
        tmp = Global.System.Activator.CreateInstance(Of T)()
    End Sub
End Class