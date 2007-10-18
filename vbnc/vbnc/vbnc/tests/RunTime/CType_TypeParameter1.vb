Class CType_TypeParameter1
    Sub Main()

    End Sub
    'This means that a type parameter T can be converted to and from Object 
    'and to and from any interface type
    Sub ToT(Of T)()
        Dim fromvalue1 As Object
        Dim fromvalue2 As i1
        Dim tovalue As T
        tovalue = CType(fromvalue1, T)
        tovalue = CType(fromvalue2, T)
    End Sub
    Sub FromT(Of T)()
        Dim fromvalue As T
        Dim tovalue1 As Object
        Dim tovalue2 As i1
        tovalue1 = CType(fromvalue, Object)
        tovalue2 = CType(fromvalue, i1)
    End Sub
    Interface I1

    End Interface
End Class