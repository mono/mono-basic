Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation6
    Class Test
        Function F(ByVal param As Object) As Integer
            Return 0
        End Function
        Shared Function Main() As Integer
            Dim t As New Test
            Dim i As Integer
            Return t.F(i)
        End Function
    End Class
End Namespace