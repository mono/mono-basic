Class CType_Array5
    Sub Main()
        Dim fromvalue As B()
        Dim tovalue As A()
        tovalue = CType(fromvalue, A())
    End Sub
    Interface A

    End Interface
    Interface B
        Inherits A
    End Interface
End Class