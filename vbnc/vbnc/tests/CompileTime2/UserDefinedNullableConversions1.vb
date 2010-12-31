Option Strict On
Class UserDefinedNullableConversions1
    Shared Function Main() As Integer
        Dim from As Nullable(Of From)
        Dim [to] As Nullable(Of [To])

        [to] = [from]
        Return 0
    End Function

    Structure From
        Dim i As Integer
        Public Shared Widening Operator CType(ByVal a As From) As [To]
            Return New [To]
        End Operator
    End Structure

    Structure [To]
        Dim i As Integer
    End Structure

End Class