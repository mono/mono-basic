Imports System
Imports System.Collections
Imports System.Reflection

Namespace TryReturn1
    Class Test
        Shared Function Main() As Integer
            Main = 2
            Try
                Throw New exception
            Catch ex As Exception
                Return 0
            End Try
            Return 1
        End Function
    End Class
End Namespace