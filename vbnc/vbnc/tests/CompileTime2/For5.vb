Imports System
Imports System.Collections
Imports System.Reflection

Namespace For5
    Class Test
        Shared Function Main() As Integer
            Dim j, k, l As Integer
            j = 10
            k = 2
            l = -3
            Dim m As Integer
            For i As Integer = j To k Step l
                m += 1
            Next
            Return m - 3
        End Function
    End Class
End Namespace