Imports System.Collections.Generic
Imports System.Collections
Imports System

<Serializable()> _
Class GetEnumerator1
    Implements IEnumerable

    Shared Function Main() As Integer
        Dim g As New GetEnumerator1
        Dim o As Object
        o = g.GetEnumerator()
    End Function

    Private m_List As New Generic.List(Of GetEnumerator1)

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return m_List.GetEnumerator
    End Function
End Class
