Imports System
Imports System.Collections
Imports System.Reflection

Namespace If2
    Class Test
        Shared Function Test(ByRef args() As Integer) As Integer
            ReDim args(1) ' args must be the same size as parameters
            Return 0
        End Function

        Shared Function Main() As Integer
            Return test(New Integer() {2, 3})
        End Function
    End Class
End Namespace