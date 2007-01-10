Imports System
Imports System.Collections
Imports System.Reflection

Class DelegateInvocation2

    Delegate Function ParseDelegate_Parent(Of T)(ByVal Parent As Object) As T

    Private Shared Function ParseImportsClause(ByVal Parent As Object) As DelegateInvocation2

    End Function

    Private Shared Sub ParseList(Of T)(ByVal ParseMethod As ParseDelegate_Parent(Of T))

    End Sub

    Shared Function Main() As Integer

        ParseList(Of DelegateInvocation2)(New ParseDelegate_Parent(Of DelegateInvocation2)(AddressOf ParseImportsClause))

        Return 0
    End Function
End Class