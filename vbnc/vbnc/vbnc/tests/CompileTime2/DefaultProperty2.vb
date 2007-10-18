Imports System
Imports System.Collections
Imports System.Reflection

Namespace DefaultProperty2
    Class Test
        Default Property P(ByVal index As String) As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property

        Default Property P(ByVal index As Integer) As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property

        Shared Function Main() As Integer

        End Function
    End Class
End Namespace