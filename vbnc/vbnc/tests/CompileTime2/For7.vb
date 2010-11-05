Imports System
Imports System.Collections
Imports System.Reflection

Namespace For7
    Class Test
        Shared Function Start() As Integer
            Return 1
        End Function
        Shared Function Main() As Integer
            For i As Integer = Start To 10
            Next
            Return 0
        End Function
    End Class
End Namespace