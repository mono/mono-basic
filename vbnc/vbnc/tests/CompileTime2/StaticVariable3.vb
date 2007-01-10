Imports System
Imports System.Collections
Imports System.Reflection

Namespace StaticVariable3
    Class Test
        Shared Function Main() As Integer
            var()
            var()
            Return var - 5
        End Function

        Shared Function Var() As Integer
            Static tmp As Integer = 2
            tmp += 1
            Return tmp
        End Function
    End Class
End Namespace