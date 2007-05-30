Imports System

Class AddressOfExpression2
    Shared Sub Main()
        a(AddressOf D)
    End Sub
    Shared Sub A(ByVal p As CrossAppDomainDelegate)

    End Sub
    Shared Sub D()

    End Sub
End Class
