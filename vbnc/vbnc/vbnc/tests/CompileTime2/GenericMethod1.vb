Imports System.Collections
Imports System.Reflection

Class GenericMethod1
    Shared Function Main() As Integer
        Dim tmpResult As New Generic.List(Of MemberInfo)
        Dim result As MemberInfo()

        result = tmpResult.ToArray

        Return result.length
    End Function
End Class