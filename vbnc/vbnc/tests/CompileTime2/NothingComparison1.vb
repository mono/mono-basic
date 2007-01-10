Imports System
Imports System.Collections
Imports System.Reflection

Namespace NothingComparison1
    Class Test
        Shared Function Char1(ByVal p As Char) As Boolean
            Return p = Nothing
        End Function

        Shared Function Char2(ByVal p As Char) As Boolean
            Return Nothing = p
        End Function

        Shared Function Main() As Integer
            If char1(Nothing) = False Then
                Return -1
            ElseIf char2(Nothing) = False Then
                Return -2
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace