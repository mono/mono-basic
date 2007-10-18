Class CType_Array3
    Sub Main()
        Dim fromvalue As Object()
        Dim tovalue As DelegateType()
        tovalue = CType(fromvalue, DelegateType())
    End Sub
    Delegate Sub DelegateType()
End Class