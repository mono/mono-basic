'Tests declarations of Namespace, Class and Module
'Includes nested declarations.

'Simple class
Class NestedDeclarations1_Class1
    Interface ci1
    End Interface

    Delegate Sub cd1()

    Structure cs1
        Public i As Integer
    End Structure

    Class cc1
    End Class

    Enum ce1
        i
    End Enum
End Class

Interface NestedDeclarations1_Interface1
    Interface ci1
    End Interface

    Delegate Sub cd1()

    Structure cs1
        Public i As Integer
    End Structure

    Class cc1
    End Class

    Enum ce1
        i
    End Enum
End Interface


Structure NestedDeclarations1_Structure1
    Public i As Integer
    Interface ci1
    End Interface

    Delegate Sub cd1()

    Structure cs1
        Public i As Integer
    End Structure

    Class cc1
    End Class

    Enum ce1
        i
    End Enum
End Structure


Enum NestedDeclarations1_Enum1
    i
End Enum

Module NestedDeclarations1_Module1
    Interface ci1
    End Interface

    Delegate Sub cd1()

    Structure cs1
        Public i As Integer
    End Structure

    Class cc1
    End Class

    Enum ce1
        i
    End Enum
End Module

Namespace NestedDeclarations1_Namespace1
    Class Class1
        Interface ci1
        End Interface

        Delegate Sub cd1()

        Structure cs1
            Public i As Integer
        End Structure

        Class cc1
        End Class

        Enum ce1
            i
        End Enum
    End Class

    Interface Interface1
        Interface ci1
        End Interface

        Delegate Sub cd1()

        Structure cs1
            Public i As Integer
        End Structure

        Class cc1
        End Class

        Enum ce1
            i
        End Enum
    End Interface


    Structure Structure1
        Public i As Integer
        Interface ci1
        End Interface

        Delegate Sub cd1()

        Structure cs1
            Public i As Integer
        End Structure

        Class cc1
        End Class

        Enum ce1
            i
        End Enum
    End Structure


    Enum Enum1
        i
    End Enum

    Module Module1
        Interface ci1
        End Interface

        Delegate Sub cd1()

        Structure cs1
            Public i As Integer
        End Structure

        Class cc1
        End Class

        Enum ce1
            i
        End Enum
    End Module


End Namespace