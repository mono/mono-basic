Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization1
    Class Test
        Shared test As Integer = 1
        Shared Function Main() As Integer
            Return test - 1
        End Function
    End Class
End Namespace