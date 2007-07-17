Public Class TestList
    Private m_List As Generic.List(Of Object)

    Public Function GetEnumerator() As System.Collections.IEnumerator
        Dim m_Queue As New Generic.LinkedList(Of Object)
        Return m_List.GetEnumerator
    End Function
End Class
