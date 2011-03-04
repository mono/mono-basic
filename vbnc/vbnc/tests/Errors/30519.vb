Public Class Test
    Public Shared Sub Main()
        Dim s As New String("test")
        Dim o As Object
        Dim t As New t(o)
    End Sub
End Class

Class T
    Sub New(ByVal s As T)

    End Sub
    Sub New(ByVal s As Test)

    End Sub
End Class