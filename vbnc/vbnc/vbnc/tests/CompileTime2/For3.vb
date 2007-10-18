Imports System
Imports System.Collections
Imports System.Reflection

Namespace For3
    Class Test
        Shared Function Main() As Integer
            Dim s1(4) As Integer
            Dim result As Integer
            s1(1) = 1

            For i As Integer = 0 To 4
                result += s1(i)
            Next

            Return result - 1
        End Function
    End Class
End Namespace