Namespace Structure3
    Structure a
        Dim value As String
    End Structure
    Public Structure b
        Dim value As Integer
    End Structure
    Friend Structure c
        Dim value As Byte
    End Structure

    Class classcontainer
        Structure a
            Dim value As String
        End Structure
        Public Structure b
            Dim value As Integer
        End Structure
        Friend Structure c
            Dim value As Byte
        End Structure
        Protected Structure d
            Dim value As structure3.a
        End Structure
        Protected Friend Structure e
            Dim value As classcontainer
        End Structure
        Private Structure f
            Dim value As interfacecontainer
        End Structure
    End Class

    Structure structurecontainer
        Dim value As Integer

        Structure a
            Dim value As String
        End Structure
        Public Structure b
            Dim value As Integer
        End Structure
        Friend Structure c
            Dim value As Byte
        End Structure
        'Protected Structure d
        '    Dim value As structure3.a
        'End Structure
        'Protected Friend Structure e
        '    Dim value As classcontainer
        'End Structure
        Private Structure f
            Dim value As interfacecontainer
        End Structure
    End Structure

    Interface interfacecontainer
        Structure a
            Dim value As String
        End Structure
        'Public Structure b
        '    Dim value As Integer
        'End Structure
        'Friend Structure c
        '    Dim value As Byte
        'End Structure
        'Protected Structure d
        '    Dim value As structure3.a
        'End Structure
        'Protected Friend Structure e
        '    Dim value As classcontainer
        'End Structure
        'Private Structure f
        '    Dim value As interfacecontainer
        'End Structure
    End Interface

    Module modulecontainer
        Structure a
            Dim value As String
        End Structure
        Public Structure b
            Dim value As Integer
        End Structure
        Friend Structure c
            Dim value As Byte
        End Structure
        'Protected Structure d
        '    Dim value As structure3.a
        'End Structure
        'Protected Friend Structure e
        '    Dim value As classcontainer
        'End Structure
        Private Structure f
            Dim value As interfacecontainer
        End Structure
    End Module


End Namespace
