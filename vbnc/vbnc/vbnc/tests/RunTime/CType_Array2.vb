Class CType_Array2
    Sub Main()
        Dim fromvalue As Object()
        Dim tovalue As IType()
        tovalue = CType(fromvalue, IType())
    End Sub
    Interface IType

    End Interface
End Class