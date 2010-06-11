Option Strict On
Class A
    Shared b As Object
    Shared Sub Main()
        Dim o As Integer = If(b, Nothing)
    End Sub
End Class