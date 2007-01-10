Imports System
Imports System.Collections
Imports System.Reflection

Namespace TryCatch2
    Class Test
        Shared Function Main() As Integer
            Try
                Throw New Exception()
            Catch ex As Exception
                Return 0
            End Try
            Return -1
        End Function
    End Class
End Namespace