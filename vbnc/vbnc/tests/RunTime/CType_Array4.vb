Class CType_Array4
    Sub Main()
        Dim fromvalue As AB()
        Dim tovalue As A()
        tovalue = CType(fromvalue, A())
    End Sub
    Class AB
        Implements A
    End Class
    Interface A

    End Interface
End Class