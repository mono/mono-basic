Class CType_Array6
    Sub Main()
        Dim fromvalue As CType_Enum6()
        Dim tovalue As Short()
        tovalue = CType(fromvalue, Short())
    End Sub
    Enum CType_Enum6 As Short
        a
    End Enum
End Class