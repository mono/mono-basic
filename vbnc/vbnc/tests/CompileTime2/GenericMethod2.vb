Imports System
Imports System.Collections
Imports System.Reflection

Class GenericMethod2
    Function A(Of X)() As Object

    End Function
    Function A(Of X, Y)() As Object
        Dim p As GenericMethod2
        Dim b As Object = p.A(Of X, Y)()
    End Function

    Shared Sub Main()

    End Sub
End Class