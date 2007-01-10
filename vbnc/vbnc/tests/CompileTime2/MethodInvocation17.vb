Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation17
    Class Test
        Function A() As Integer

        End Function
        Function B() As Integer
            Return A
        End Function
        Shared Function Main() As Integer
            Return 0
        End Function
    End Class
End Namespace