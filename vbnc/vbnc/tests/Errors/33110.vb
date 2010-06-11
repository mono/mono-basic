Option Strict On
Class A
    Shared Sub Main()
        Dim i As Nullable(Of Integer)
        Dim o As Integer = If(i, 1UL)
    End Sub
End Class