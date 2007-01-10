Imports System
Imports System.Collections
Imports System.Reflection

Namespace ForEach2
    Class Test
        Shared Function Main() As Integer
            Dim list As New ArrayList
            For Each value As Object In list
                Return 1
            Next
            Return 0
        End Function
    End Class
End Namespace