Class C1

    Structure Point
        Public X As Integer
        Public Y As Integer
    End Structure

    Private Sub M(ByRef N As Short, ByRef Arr() As Point, ByRef X As Short)
        Arr(N).X = X
    End Sub

End Class

Class C2

    Class Point
        Public X As Integer
        Public Y As Integer
    End Class

    Private Sub M(ByRef N As Short, ByRef Arr() As Point, ByRef X As Short)
        Arr(N).X = X
    End Sub

End Class
