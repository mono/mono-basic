Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethod4
    Class List(Of T)
        Inherits Generic.List(Of T)

        ReadOnly Property Item(ByVal i As Integer) As T
            Get
                Return DirectCast(MyBase.Item(i), T)
            End Get
        End Property
    End Class

    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace