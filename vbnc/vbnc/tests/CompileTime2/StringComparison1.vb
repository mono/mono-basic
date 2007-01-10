Imports System
Imports System.Collections
Imports System.Reflection

Namespace StringComparison1
    Class Test
        Shared Function Main() As Integer
            Dim b As Boolean
            Dim s1, s2 As String
            s1 = "A"
            s2 = "a"
            b = s1 = s2
            Return CInt(b)
        End Function
    End Class
End Namespace