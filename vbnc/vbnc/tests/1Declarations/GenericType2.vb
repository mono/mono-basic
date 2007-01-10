Class GenericType2
    Class Gen(Of T)
    End Class
    Shared Function Main() As Integer
        Dim a As New gen(Of Integer)
        Dim b As gen(Of Integer) = New gen(Of Integer)
        Dim c As gen(Of Integer)
        c = New gen(Of Integer)

    End Function
End Class