Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation9
    Class Test
        Shared Function Test(ByVal ParamArray V() As Integer) As Integer
            Return 0
        End Function
        Shared Function Main() As Integer
            Return test(New Integer() {0})
        End Function
    End Class
End Namespace