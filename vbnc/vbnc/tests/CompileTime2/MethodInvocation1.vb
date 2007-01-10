Imports System.Collections
Imports System.Reflection

Class MethodInvocation1
    Shared Function Main() As Integer
        Dim m_OriginalGroup As Generic.List(Of MemberInfo)
        Dim lst As New Generic.List(Of MethodBase)
        m_OriginalGroup = New Generic.List(Of MemberInfo)(lst.ToArray)
        Return m_OriginalGroup.Count
    End Function
End Class