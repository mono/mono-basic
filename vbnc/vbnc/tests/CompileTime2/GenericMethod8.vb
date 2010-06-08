Imports System
Imports System.Collections
Imports System.Reflection

Class GenericMethod8
    Shared Sub Main()
        System.Array.BinarySearch(Of String)(New String() {"a", "b"}, "c")
    End Sub
End Class