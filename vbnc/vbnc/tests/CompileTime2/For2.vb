Imports System
Imports System.Collections
Imports System.Reflection

Namespace For2
    Class Test
        Shared Function Main() As Integer
            Dim list As Integer() = New Integer() {0}
            For i As Integer = 0 To list.getupperbound(0)
                Return 0
            Next
            Return 1
        End Function
    End Class
End Namespace