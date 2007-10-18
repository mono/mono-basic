Imports System.Collections
Imports System.Reflection

Class DelegateAsMethodParameter1

    Private Shared m_Group As New System.Collections.Generic.List(Of MemberInfo)

    Private Shared Function IsNothing(Of T)(ByVal Value As T) As Boolean
        Return value Is Nothing
    End Function

    Shared Function Main() As Integer
        m_Group.Add(Nothing)
        m_Group.RemoveAll(AddressOf IsNothing(Of MemberInfo))
        Return m_group.Count
    End Function
End Class