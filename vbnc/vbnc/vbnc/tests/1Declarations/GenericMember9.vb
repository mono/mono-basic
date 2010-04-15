Imports System.Collections

Namespace GenericMember9
    Class B
        Public F As String
        Public Sub Initialize(ByVal o As Object)

        End Sub
    End Class

    Class A(Of T As B)
        Public m_List As New Generic.List(Of T)

        Function M() As Integer
            Dim g As New A(Of B)
            For i As Integer = 0 To m_List.Count - 1
                m_List(i).Initialize(Nothing)
            Next
            Return 0
        End Function

        Shared Function Main() As Integer
            Return 0
        End Function
    End Class
End Namespace