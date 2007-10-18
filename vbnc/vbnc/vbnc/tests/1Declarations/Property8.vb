
Class Property7
    Shared Function Main() As Integer
        Dim nullable As System.Nullable(Of Integer)
        'System.Nullable has a private field called 'hasValue' and a public property called 'HasValue' (differs only by case).
        Dim obj As Object
        obj = nullable.value
        Return CInt(obj IsNot Nothing)
    End Function
End Class
