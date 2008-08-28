Imports System.Collections

Public Class Generic4
    Shared Function Main() As Integer
    End Function

    Sub M()
        Dim o As String
        o = Me.A(Of String)()
    End Sub

    Function A(Of T)() As T

    End Function
End Class
