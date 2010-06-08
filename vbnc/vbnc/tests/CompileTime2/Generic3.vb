Imports System.Collections

Public Class Generic3
    Private Shared i As New Generic.List(Of String)
    Private Shared j As Integer
    Shared Function Main() As Integer
        T(i.Count)
        Return j
    End Function

    Shared Sub T(ByVal i As Integer)
        j = i
    End Sub
End Class
