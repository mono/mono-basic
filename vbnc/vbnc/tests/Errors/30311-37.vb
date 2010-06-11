Option Strict On
Class ImplicitNullableConversions1
    Shared Function Main() As Integer
        Dim f As Nullable(Of Double)
        Dim c As Nullable(Of Char)
        f = c
    End Function

End Class