Option Strict On
Class A
    Shared b As Long
    Shared Sub Main()
        Dim o As Integer = If(b, Nothing)
    End Sub
End Class