Imports System
Imports System.Collections
Imports System.Reflection

Namespace PropertyAccess4
    Class Test
        Public Property P1() As Integer
            Get
            End Get
            Protected Set(ByVal value As Integer)
            End Set
        End Property
        Property P2() As String
            Get
            End Get
            Private Set(ByVal value As String)
            End Set
        End Property
        Private Property P3() As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Shared Sub Main()

        End Sub
    End Class
End Namespace