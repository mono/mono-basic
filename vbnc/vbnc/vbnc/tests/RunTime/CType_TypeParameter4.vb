Class CType_TypeParameter4
    Sub Main()
        'No test here yet.
    End Sub
    'An array whose element type is a type parameter with a class constraint C 
    'has the same covariant array conversions as an array whose element type is C.
    Sub ToTC(Of T As C)()
        Dim fromvalue As C()
        Dim tovalue As T()
        tovalue = CType(fromvalue, T())
    End Sub
    Class C

    End Class
End Class