Class CType6
    Sub Main()
        Dim fromvalue As Object
        Dim tovalue As DelegateType
        tovalue = CType(fromvalue, DelegateType)
    End Sub
    Delegate Sub DelegateType()
End Class