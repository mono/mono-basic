Namespace Class2
    Class a

    End Class
    Public Class b
        Shadows Class shadowsclass1
        End Class
    End Class
    Friend Class c

    End Class
    Class classcontainer
        Class a
        End Class
        Public Class b
        End Class
        Friend Class c
        End Class
        Protected Class d
        End Class
        Protected Friend Class e
        End Class
        Private Class f
        End Class
    End Class

    Structure structurecontainer
        Dim value As Integer
        Class a
        End Class
        Public Class b
        End Class
        Friend Class c
        End Class
        'Protected class d
        'End class
        'Protected Friend class e
        'End class
        Private Class f
        End Class
    End Structure

    Class interfacecontainer
        Class a
        End Class
        Public Class b
        End Class
        Friend Class c
        End Class
        'Protected class d
        'End class
        'Protected Friend class e
        'End class
        'Private class f
        'End class
    End Class

    Module modulecontainer
        Class a
        End Class
        Public Class b
        End Class
        Friend Class c
        End Class
        'Protected class d
        'End class
        'Protected Friend class e
        'End class
        Private Class f
        End Class
    End Module


    Class base1
    End Class
    Class base2
    End Class
    Class base3
    End Class
    Class derived1
        Inherits base1
    End Class
    Class derived2
        Inherits base2
    End Class
    Class derived3
        Inherits derived1
    End Class
    Class derived4
        Inherits derived2
    End Class
    Class derived5
        Inherits derived4
    End Class

    MustInherit Class mustinherit1
    End Class
    NotInheritable Class notinheritable1
    End Class
    Class mustinherit2
        Inherits mustinherit1
    End Class

End Namespace
