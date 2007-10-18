Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation13
    Class Test
        Public T As Integer

        Shared Function ByRefTest(ByRef Value As Test) As Integer
            Return value.T
        End Function

        Shared Function Main() As Integer
            Return ByRefTest(New Test)
        End Function
    End Class
End Namespace