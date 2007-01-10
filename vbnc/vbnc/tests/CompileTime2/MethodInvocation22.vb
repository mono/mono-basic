Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation22
    Enum E
        value
    End Enum
    Class Test

        Shared Sub F(ByVal value As System.Enum)

        End Sub
        Shared Function Main() As Integer
            f(e.value)
        End Function
    End Class
End Namespace