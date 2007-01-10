Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethod3
    Class Test
        Shared Function ParseList(Of T)(ByVal param As T) As Boolean
            Dim result As Boolean
            result = param Is Nothing
            result = param IsNot Nothing
            Return result
        End Function

        Shared Function Main() As Integer
            If ParseList(Of Integer)(0) Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
End Namespace