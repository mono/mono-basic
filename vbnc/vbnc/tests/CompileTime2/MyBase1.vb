Imports System
Imports System.Collections
Imports System.Reflection

Namespace MyBase1
    Class Base
        Public B_PG As Boolean
        Public B_PS As Boolean
        Public B_F As Boolean
        Public B_S As Boolean

        Overridable Property P() As Boolean
            Get
                b_pg = True
            End Get
            Set(ByVal value As Boolean)
                b_ps = True
            End Set
        End Property

        Overridable Function F() As Boolean
            b_f = True
        End Function

        Overridable Sub S()
            b_s = True
        End Sub
    End Class
    Class Test
        Inherits Base

        Overrides Property P() As Boolean
            Get
                Return MyBase.P
            End Get
            Set(ByVal value As Boolean)
                MyBase.P = value
            End Set
        End Property

        Overrides Function F() As Boolean
            Return MyBase.F
        End Function

        Overrides Sub S()
            MyBase.S()
        End Sub

        Shared Function Main() As Integer
            Dim t As New test
            Dim tmp As Boolean

            t.p = True
            tmp = t.p
            t.f()
            t.s()

            If t.b_pg = False Then Return -1
            If t.b_ps = False Then Return -2
            If t.b_f = False Then Return -3
            If t.b_s = False Then Return -4

            Return 0
        End Function
    End Class
End Namespace