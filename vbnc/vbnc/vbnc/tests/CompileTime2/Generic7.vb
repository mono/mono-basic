Imports System.Collections

Public Class Generic6
    Shared Function Main() As Integer
        Dim o As generic6() = C.F(Of Generic6)(Nothing, 2)
    End Function

    Class C
        Shared Function F(Of T)(ByVal var As T, ByVal count As Integer) As T()

        End Function
    End Class
End Class
