Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericProperty1

    Class Test
        Shared Function Main() As Integer
            Dim test As New BaseList(Of Object)
            Return Test.Count
        End Function
    End Class

    Class BaseBaseList(Of T)
        ReadOnly Property Count() As Integer
            Get
                Return 0
            End Get
        End Property
    End Class

    Class BaseList(Of T)
        Inherits BaseBaseList(Of T)

    End Class
End Namespace