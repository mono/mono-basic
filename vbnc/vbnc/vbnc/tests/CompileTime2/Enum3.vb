Imports System
Imports System.Collections
Imports System.Reflection

Namespace Enum3
    Class Test
        Shared Function Main() As Integer
            Dim b As Boolean
            Dim e As [Enum]
            b = e IsNot Nothing
            If b Then
                Return 1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace