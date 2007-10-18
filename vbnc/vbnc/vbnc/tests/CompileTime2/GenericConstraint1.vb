Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Reflection.Emit

Namespace GenericConstraint1

    Class Base(Of T As C)

    End Class

    Class Derived(Of T As {C, I})
        Inherits Base(Of T)
    End Class

    Interface I

    End Interface
    Class C

        Shared Function Main() As Integer

        End Function
    End Class
End Namespace