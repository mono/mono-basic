Imports System
Imports System.Collections
Imports System.Reflection

Namespace OverrideImplementedMethod1
    Interface I1
        Function Method() As Integer
    End Interface
    Class C1
        Implements I1

        Overridable Function IMethod() As Integer Implements I1.Method
            Return 1
        End Function
    End Class
    Class C2
        Inherits C1

        Overrides Function IMethod() As Integer
            Return 0
        End Function
    End Class

    Class Test
        Shared Function Main() As Integer
            Dim c As New C2
            Return c.IMethod
        End Function
    End Class
End Namespace