Class CType_TypeParameter2
    Sub Main()

    End Sub
    'A type parameter with a class constraint C defines additional conversions from the type parameter to C and its base classes, and vice versa. 
    Sub ToT_A(Of T As A)()
        Dim fromvalue1 As A
        Dim tovalue As T
        tovalue = CType(fromvalue1, T)
    End Sub
    Sub ToT_B(Of T As B)()
        Dim fromvalue1 As A
        Dim fromvalue2 As B
        Dim tovalue As T
        tovalue = CType(fromvalue1, T)
        tovalue = CType(fromvalue2, T)
    End Sub
    Sub FromT_A(Of T As A)()
        Dim fromvalue As T
        Dim tovalue1 As A

        tovalue1 = CType(fromvalue, A)
    End Sub
    Sub FromT_B(Of T As B)()
        Dim fromvalue As T
        Dim tovalue1 As A
        Dim tovalue2 As B

        tovalue1 = CType(fromvalue, A)
        tovalue2 = CType(fromvalue, B)
    End Sub


    Class A

    End Class
    Class B
        Inherits A

    End Class
End Class