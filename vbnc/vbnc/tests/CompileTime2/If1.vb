Imports System
Imports System.Collections
Imports System.Reflection

Namespace If1
    Class Test
        Shared Function Main() As Integer
            If False Then
                Return 1
            Else
                Return 0
            End If
            Return 2
        End Function
    End Class
End Namespace