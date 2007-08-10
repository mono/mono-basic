Public Class ArrayTypeC7
    Structure Point
        Public X As Integer
        Public Y As Integer
        Public Sub New(ByVal X As Integer, ByVal Y As Integer)
            Me.X = X
            Me.Y = Y
        End Sub
    End Structure

    Function Main() As Integer
        Dim v() As Point
        v(0) = New Point(1, 2)
        Return v(0).X - 1 + v(0).Y - 2
    End Function
End Class