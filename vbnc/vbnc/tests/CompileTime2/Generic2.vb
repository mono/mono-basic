Imports System.Collections

Public Class Generic2
    Inherits Generic.Dictionary(Of String, System.Type)

    Function TypesAsArray() As Type()
        Dim result() As Type
        ReDim result(Me.Count - 1)
        MyBase.Values.CopyTo(result, 0)
        Return result
    End Function


    Shared Function Main() As Integer

    End Function
End Class
