Class CType5
    Sub Main()
        Dim fromvalue As Object
        Dim tovalue As ArrayValue()
        tovalue = CType(fromvalue, ArrayValue())
    End Sub
    Enum ArrayValue
        i = 2
    End Enum
End Class