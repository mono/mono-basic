Class Bug444995AndyH
    Shared Sub Main(ByVal args() As String)
        Dim x(5) As Integer
        '
        '#If Not SkipVbncFailures Then
        Array.Resize(x, 10)
        Aaaa.One(1)
        Aaaa.Arr(x)
        '#End If
        ' This works: explicit type arguments
        ' (line: 11)
        Array.Resize(Of Integer)(x, 10)
        Aaaa.One(Of Integer)(1)
        Aaaa.Arr(Of Integer)(x)
    End Sub
End Class


Class Aaaa
    Shared Sub One(Of T)(ByVal p As T)
    End Sub

    Shared Sub Arr(Of T)(ByVal p As T())
    End Sub
End Class