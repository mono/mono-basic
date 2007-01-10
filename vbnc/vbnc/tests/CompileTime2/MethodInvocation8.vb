Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation8
    Class Test
        Shared Function Test(ByVal v As Integer()) As Integer
            Return 0
        End Function
        Shared Function Main() As Integer
            Return test(New Integer() {0})
        End Function
    End Class
End Namespace