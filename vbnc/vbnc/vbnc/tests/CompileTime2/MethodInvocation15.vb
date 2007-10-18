Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation15
    Class Test
        Shared ReadOnly Property P1() As Integer
            Get
                Return 0
            End Get
        End Property
        Shared Function Test(ByVal value As Integer) As Integer
            Return 0
        End Function
        Shared Function Main() As Integer
            Return Test(P1)
        End Function
    End Class
End Namespace