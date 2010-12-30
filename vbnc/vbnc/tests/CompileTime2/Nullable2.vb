Class Nullable2
    Function Compare() As Boolean
        Dim a As nullable(Of Date)
        Dim b As nullable(Of Date)
        Return Nullable.Compare(a, b)
    End Function
End Class