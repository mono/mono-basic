Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation18
    Class Modifiers
        ReadOnly Property [Is](ByVal index As Integer) As Boolean
            Get

            End Get
        End Property
    End Class
    Class Base
        ReadOnly Property Modifiers() As Modifiers
            Get
            End Get
        End Property
    End Class

    Class Test
        Inherits Base

        ReadOnly Property CanRead() As Boolean
            Get
                Return Modifiers.Is(1)
            End Get
        End Property

        Shared Function Main() As Integer

        End Function
    End Class
End Namespace