
Class CType_Interface1
    Sub Main()
        Dim fromvalue As i2
        Dim tovalue As I1
        tovalue = CType(fromvalue, I1)
    End Sub

    Interface I1

    End Interface

    Interface I2
        Inherits i1
    End Interface
End Class