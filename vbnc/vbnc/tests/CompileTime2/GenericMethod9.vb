Imports System
Imports System.Collections
Imports System.Reflection

Class GenericMethod8
    Shared Function A(Of T)() As T

    End Function

    Shared Sub Main()
        Dim b As String = A(Of String)()
    End Sub
End Class