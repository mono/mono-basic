Imports System.Collections
Imports System.Reflection

Class MethodInvocation3

    Shared Function Test(ByVal Methods As Generic.List(Of MemberInfo)) As Integer
        Return 1
    End Function

    Shared Function Test(ByVal ParamArray a() As MemberInfo) As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Dim v As New Generic.List(Of MethodInfo)
        Return Test(v.Toarray)
    End Function
End Class