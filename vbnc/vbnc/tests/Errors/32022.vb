Imports System
Imports System.Collections
Imports System.Reflection

Namespace For7
    Class Test
        Shared Event Foo(ByVal i As Integer)
        Shared Function Start() As Integer
            Return 1
        End Function
        Shared Function Main() As Integer
            For i As Integer = Foo To 10
            Next
            Return 0
        End Function
    End Class
End Namespace