Imports System.Threading

Class AddressOfExpression1
    Private Sub StartThread()
        Dim m_Queue As Object
        Dim m_Thread As Thread
        SyncLock m_Queue
            If m_Thread Is Nothing Then
                m_Thread = New Threading.Thread(AddressOf StartThread)
                m_Thread.Start()
            End If
        End SyncLock
    End Sub
End Class
