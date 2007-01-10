Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRef8
    Class Test
        Function BR1(ByRef v As Object) As Boolean
            Return v Is Nothing
        End Function
        Function BR2(ByRef v As Object) As Boolean
            Return v IsNot Nothing
        End Function
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace