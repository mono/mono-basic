Imports System
Imports System.Collections
Imports System.Reflection

Namespace TryCatch1
    Class Test
        Shared Function Main() As Integer
            Try

            Catch ex As OutOfMemoryException

            End Try
        End Function
    End Class
End Namespace