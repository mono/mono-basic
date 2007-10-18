Class CType_TypeParameter3
    Sub Main()

    End Sub
    'An array whose element type is a type parameter with an interface constraint I 
    'has the same covariant array conversions as an array whose element type is I. 
    'These tests fail in vbc??
    'Sub ToTI(Of T As I)()
    '    Dim fromvalue As I()
    '    Dim tovalue As T()
    '    tovalue = CType(fromvalue, T())
    'End Sub
    'Sub FromTI(Of T As I)()
    '    Dim fromvalue As T()
    '    Dim tovalue As I()
    '    tovalue = CType(fromvalue, I())
    'End Sub
    'Interface I

    'End Interface

    'An array whose element type is a type parameter with a class constraint C 
    'has the same covariant array conversions as an array whose element type is C.
    Sub FromTC(Of T As C)()
        Dim fromvalue As T()
        Dim tovalue As C()
        tovalue = CType(fromvalue, C())
    End Sub
    Class C

    End Class
End Class