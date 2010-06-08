Imports System
Imports System.Collections
Imports System.Reflection

Namespace TryCatch4
    Class Test
        Shared Function Main() As Integer
            Dim i As Integer
            Try
                i = 1
            Catch ex As NotImplementedException
                i = 2
            Catch ex As ArgumentOutOfRangeException
                i = 3
            End Try
        End Function
    End Class
End Namespace