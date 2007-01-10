Imports System
Imports System.Collections
Imports System.Reflection

Namespace StaticVariable1
    Class Test
        Shared Function Main() As Integer
            var()
            var()
            Return var - 3
        End Function

        Shared Function Var() As Integer
            Static tmp As Integer
            tmp += 1
            Return tmp
        End Function
    End Class
End Namespace