Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation16
    Class T
        Inherits ArrayList

        ReadOnly Property Length() As Integer
            Get
                Return Count
            End Get
        End Property
    End Class

    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace