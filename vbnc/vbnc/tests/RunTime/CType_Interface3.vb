Class CType_Interface3
    Sub Main()
        Dim fromvalue As FromType
        Dim tovalue As ToType
        tovalue = CType(fromvalue, ToType)
    End Sub

    Interface FromType

    End Interface

    Structure ToType
        Implements fromtype
        Dim i As Integer
    End Structure
End Class