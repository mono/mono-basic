Imports System
Imports System.Collections

Class GenericConstructor1
    Shared Function Main() As Integer
        Dim cache As New Generic.Dictionary(Of String, Type)(System.StringComparer.OrdinalIgnoreCase)
        Return cache.Count
    End Function
End Class