Imports System
Imports System.Collections
Imports System.Reflection

Namespace StaticVariable2
    Class Test
        Shared Function Main() As Integer
            Dim t As New test
            t.var()
            t.var()
            Return t.var - 3
        End Function

        Function Var() As Integer
            Static tmp As Integer
            tmp += 1
            Return tmp
        End Function
    End Class
End Namespace