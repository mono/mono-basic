Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodOverload1
    Class Test
        Enum KS
            value
        End Enum
        Overloads Sub F()

        End Sub
        Overloads Sub f(ByVal P As Integer)

        End Sub

        Shared Function Main() As Integer

        End Function
    End Class
End Namespace