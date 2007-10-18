Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization5
    Class Test
        Dim value(2) As Integer
        Function T() As Integer
            value(0) = 2
            Return value(0) - 2
        End Function
        Shared Function Main() As Integer
            Dim t As New Test
            Return t.t
        End Function
    End Class
End Namespace