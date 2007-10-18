Class TypeArguments9
    Class Test1
        Inherits system.collections.generic.list(Of String)
    End Class
    Class Test2(Of A)
        Inherits system.collections.generic.list(Of A)
    End Class
    Class Test3(Of B)
        Inherits system.collections.generic.dictionary(Of B, String)
    End Class
    Class Test4(Of C, D)
        Inherits system.collections.generic.dictionary(Of C, D)
    End Class
End Class