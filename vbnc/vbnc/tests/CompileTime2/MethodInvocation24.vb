Module MethodInvocation24
    Public Sub M(ByRef FromArray(,) As Short, ByRef ToArray(,,) As Short)

    End Sub
    Public Sub M(ByRef FromArray(,,) As Short, ByRef ToArray(,) As Short)

    End Sub
    Public Sub M(ByRef FromArray(,,) As Short, ByRef ToArray(,,) As Short)

    End Sub

    Public Sub Main()
        Dim two As Short(,)
        Dim three As Short(,,)
        M(two, three)
        M(three, two)
        M(three, three)
    End Sub
End Module