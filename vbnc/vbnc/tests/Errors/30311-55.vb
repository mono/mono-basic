Option Strict On
Class UserDefinedNullableConversions1
    Shared Function Main() As Integer
        Dim d As Nullable(Of Decimal)
        Dim c As Nullable(Of Char)
        ' vbc compiles char? -> decimal? using a user defined char->decimal widening operator on decimal
        d = c
    End Function

End Class