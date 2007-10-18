Class GenericType1
    Class Gen(Of T)
    End Class
    Shared Function Main() As Integer
        Dim a As gen(Of Integer)
        a = New gen(Of Integer)
    End Function
End Class