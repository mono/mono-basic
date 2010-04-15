Imports System.Collections.Generic
Imports System.Reflection

Public Class GenericInterface1
    Shared Function Main() As Integer
        Dim i As IList(Of MemberInfo)
        If i IsNot Nothing Then
            Return i.Count
        Else
            Return 0
        End If
    End Function
End Class
