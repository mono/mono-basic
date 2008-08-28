Imports System

Public Class Generic9
    Shared Function Main() As Integer
        Dim o As Object = New Predicate(Of String)(AddressOf T)
    End Function

    Shared Function T(ByVal P As String) As Boolean
        Return True
    End Function
End Class
