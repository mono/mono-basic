Class CType_ValueType1
    Sub Main()
        Dim fromvalue As s
        Dim tovalue As I
        tovalue = CType(fromvalue, I)
    End Sub

    Interface I

    End Interface

    Structure S
        Implements I
        Dim i As Integer
    End Structure
End Class