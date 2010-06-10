Imports System.Collections.Generic

Class DefaultProperty7
    Shared Function Main() As Integer
        Dim h As New Dictionary(Of String, Integer)
        h.Item("foo") = 2
        h.Item("foo") += 3
        Return h("foo") - 5
    End Function
End Class
