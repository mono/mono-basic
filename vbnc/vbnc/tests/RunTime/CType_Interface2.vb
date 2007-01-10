Class CType_Interface2
    Sub Main()
        Dim fromvalue As FromType
        Dim tovalue As ToType
        tovalue = CType(fromvalue, ToType)
    End Sub

    Interface FromType

    End Interface

    Class ToType

    End Class
End Class