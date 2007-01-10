Imports System
Imports System.Collections
Imports System.Reflection

Namespace For6
    Class Test
        Shared Function Main() As Integer
            Dim j As Integer
            For i As Integer = 10 To 5 Step -1
                j -= 1
            Next
            Return j + 6
        End Function
    End Class
End Namespace