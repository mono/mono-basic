Imports System

Class Nullable1
    Shared Function Main() As Integer
        Dim v As Nullable(Of Integer)
        v = 0
        Return v.Value
    End Function
End Class