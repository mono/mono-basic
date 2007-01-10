Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericForEach1
    Class G(Of T)
        Sub Test()
            Dim list As New Generic.List(Of T)
            For Each item As T In list

            Next
        End Sub
    End Class

    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace