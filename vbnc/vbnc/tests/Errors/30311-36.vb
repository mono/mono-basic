Class ImplicitNullableConversions1
    Shared Function Main() As Integer
        Dim c As Nullable(Of Char)
        Dim boo As Nullable(Of Boolean)

        boo = c
    End Function


End Class