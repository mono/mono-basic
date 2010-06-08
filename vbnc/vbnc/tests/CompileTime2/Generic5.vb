Imports System.Collections

Public Class Generic5
    Shared dic As New generic.dictionary(Of Integer, String)

    Shared Function Main() As Integer
        Dim o As Object
        dic.add(1, "2")
        o = dic(1)
        Return 0
    End Function

End Class
