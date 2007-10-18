Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization3
    Class valuecontainer
        Public value As Integer

        Sub New()
            value = -1
        End Sub

        Sub New(ByVal p1 As Integer)
            value = p1
        End Sub

        Sub New(ByVal p1 As Integer, ByVal p2 As Integer)
            Me.new(p1 * p2)
        End Sub
    End Class
    Class Test
        Public value As Integer

        Public v1 As Integer = 1
        Public v2 As valuecontainer = New valuecontainer
        Public v3 As New valuecontainer()
        Public v4 As New valuecontainer(2)
        Public v5 As New valuecontainer(3, 4)

        Shared Function Main() As Integer
            Dim t As New test

            If t.v1 <> 1 Then
                Return -1
            End If
            If t.v2.value <> -1 Then
                Return -2
            End If
            If t.v3.value <> -1 Then
                Return -3
            End If
            If t.v4.value <> 2 Then
                Return -4
            End If
            If t.v5.value <> 12 Then
                Return -5
            End If
            Return 0
        End Function
    End Class
End Namespace