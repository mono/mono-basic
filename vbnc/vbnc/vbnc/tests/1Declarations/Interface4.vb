Namespace Interface4
    Interface a

    End Interface
    Public Interface b
        Shadows Interface shadowsinterfacea
        End Interface
    End Interface
    Friend Interface c

    End Interface
    Class classcontainer
        Interface a
        End Interface
        Public Interface b
        End Interface
        Friend Interface c
        End Interface
        Protected Interface d
        End Interface
        Protected Friend Interface e
        End Interface
        Private Interface f
        End Interface
    End Class

    Structure structurecontainer
        Dim value As Integer
        Interface a
        End Interface
        Public Interface b
        End Interface
        Friend Interface c
        End Interface
        'Protected Interface d
        'End Interface
        'Protected Friend Interface e
        'End Interface
        Private Interface f
        End Interface
    End Structure

    Interface interfacecontainer
        Interface a
        End Interface
        Public Interface b
        End Interface
        Friend Interface c
        End Interface
        'Protected Interface d
        'End Interface
        'Protected Friend Interface e
        'End Interface
        'Private Interface f
        'End Interface
    End Interface

    Module modulecontainer
        Interface a
        End Interface
        Public Interface b
        End Interface
        Friend Interface c
        End Interface
        'Protected Interface d
        'End Interface
        'Protected Friend Interface e
        'End Interface
        Private Interface f
        End Interface
    End Module


    Interface base1

    End Interface
    Interface base2

    End Interface
    Interface base3

    End Interface
    Interface derived1
        Inherits base1
    End Interface
    Interface derived2
        Inherits base2
    End Interface
    Interface derived3
        Inherits derived1, derived2
    End Interface
    Interface derived4
        Inherits derived2, base3
    End Interface
    Interface derived5
        Inherits derived3, derived4
    End Interface

End Namespace
