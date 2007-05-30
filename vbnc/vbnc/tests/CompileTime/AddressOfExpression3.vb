Imports System.Threading

Class AddressOfExpression3
    Private Sub StartThread()
        Dim m_Queue As Object
        Dim m_Thread As Thread
        SyncLock m_Queue
            If m_Thread Is Nothing Then
                m_Thread = New Threading.Thread(AddressOf A)
                m_Thread.Start()
            End If
        End SyncLock
    End Sub

    Sub A()

    End Sub
    Sub A(ByVal a As Integer)

    End Sub
    Sub A(ByVal a As Integer, ByVal b As Object)

    End Sub
End Class
