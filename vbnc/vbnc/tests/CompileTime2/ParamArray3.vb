Imports System
Imports System.Collections
Imports System.Reflection

Namespace ParamArray3
    Class Test
        Shared Function Main() As Integer
            Return p(1, 2, 3)
        End Function

        Shared Function P(ByVal ParamArray v() As Integer) As Integer
            If v Is Nothing Then Return -1
            If v.length <> 3 Then Return -2
            If v(0) <> 1 Then Return -3
            If v(1) <> 2 Then Return -4
            If v(2) <> 3 Then Return -5
            Return 0
        End Function
    End Class
End Namespace