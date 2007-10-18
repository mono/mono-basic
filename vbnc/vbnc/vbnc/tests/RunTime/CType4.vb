Class CType4
    Sub Main()
        Dim fromvalue As Object
        Dim tovalue As EnumType
        tovalue = CType(fromvalue, EnumType)
    End Sub
    Enum EnumType
        i = 2
    End Enum
End Class