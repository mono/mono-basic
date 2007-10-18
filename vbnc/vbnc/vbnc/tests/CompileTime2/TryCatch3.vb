Imports System
Imports System.Collections
Imports System.Reflection

Namespace TryCatch3
    Class Test
        Shared Function Main() As Integer
            Try
                Throw New Exception
            Catch ex As Exception
                Console.WriteLine(ex.message)
                Return 0
            End Try
        End Function
    End Class
End Namespace