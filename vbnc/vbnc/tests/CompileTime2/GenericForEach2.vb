Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericForEach2
    Class T
        Structure S
            Dim value As Integer
        End Structure
        Sub Test()
            Dim v As arraylist
            Dim i As Integer
            For Each obj As s In v
                i = 100000000
            Next
        End Sub
    End Class
    Class Test
        Private Shared m_TypeDescriptorsOfTypes As Generic.Dictionary(Of Type, Integer)
        Shared Sub New()
            m_TypeDescriptorsOfTypes = New Generic.Dictionary(Of Type, Integer)
        End Sub
        Shared Function Test() As Integer
            For Each item As Generic.KeyValuePair(Of Type, Integer) In m_TypeDescriptorsOfTypes
            Next
            Return 0
        End Function
        Shared Function Main() As Integer
            Return test
        End Function
    End Class
End Namespace