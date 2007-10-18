Imports System
Imports System.Collections
Imports System.Reflection

Namespace ParamArray4
    Class Test
        Shared Function P(ByVal ParamArray v() As Integer) As Integer
            If v Is Nothing Then Return -1
            If v.length <> 0 Then Return -2
            Return 0
        End Function

        Shared Function Main() As Integer
            If P() <> 0 Then Return 1
            If p(Nothing) <> -1 Then Return 2
        End Function
    End Class
End Namespace