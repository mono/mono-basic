Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericInherits1
    Class Base(Of T)

    End Class

    Class Derived(Of V)
        Inherits Base(Of V)

        Function Test2() As V

        End Function
    End Class

    Class Derived2(Of S)
        Inherits Derived(Of S)

        Function Test() As S
            Return Test2
        End Function
    End Class

    Class T
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace
