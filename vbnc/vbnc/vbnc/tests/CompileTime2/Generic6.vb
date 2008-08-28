Imports System.Collections

Public Class Generic6
    Shared Function Main() As Integer
        Dim C As New C(Of String)
        Dim o As Object
        o = c.F
        o = c.P
        c.P = New Generic.List(Of String)
    End Function

    Class C(Of X)
        Function F() As Generic.List(Of X)

        End Function
        Property P() As Generic.List(Of X)
            Get

            End Get
            Set(ByVal value As Generic.List(Of X))

            End Set
        End Property
    End Class
End Class
