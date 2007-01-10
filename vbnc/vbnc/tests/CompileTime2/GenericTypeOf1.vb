Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericTypeOf1
    Class Test
        Function V(Of T)() As Boolean
            Dim var As T
            Return TypeOf var Is T
        End Function
        Shared Function Main() As Integer
            Return 0
        End Function
    End Class
End Namespace