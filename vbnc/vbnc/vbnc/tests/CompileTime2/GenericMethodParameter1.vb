Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethodParameter1
    Class Test
        Function G(Of T)() As Generic.List(Of T)
            Return New Generic.List(Of T)()
        End Function

        Shared Function Main() As Integer
            Dim t As New Test
            Return t.G(Of Test).Count
        End Function
    End Class
End Namespace