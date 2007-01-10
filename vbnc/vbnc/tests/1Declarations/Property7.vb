
Class Property7
    Shared Function Main() As Integer
        Dim nullable As System.Nullable(Of Integer)
        'System.Nullable has a private field called 'hasValue' and a public property called 'HasValue' (differs only by case).
        Return CInt(nullable.HasValue)
    End Function
End Class
