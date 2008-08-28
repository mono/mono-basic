Imports System.Collections.Generic

Namespace GenericType3
    Class A(Of X, Y)
        Public C As List(Of Y)

        Shared Sub M()
            c.add(Nothing)
        End Sub
    End Class
End Namespace