Option Strict Off
Module Module1

    Private Sub SyncLockTest()

        Dim UsedInSyncLock As Object
        Dim UnusedInSyncLock As Object

        SyncLock UsedInSyncLock

        End SyncLock

    End Sub

End Module