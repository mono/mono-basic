Class SyncLock1
	Sub Test
		Dim o As Object
		SyncLock o
			Dim i As Integer
			i = 7
		End SyncLock
	End Sub
	Sub Test2
	      	Dim o As SyncLock1
		SyncLock o
			Dim i As Integer
			i = 8
		End SyncLock
	End Sub
	Sub Test3
	      	Dim o As SyncLock1
		SyncLock o.Tester.Tester.Tester.Tester
			Dim i As Integer
			i = 8
		End SyncLock
	End Sub
	Public Tester as SyncLock1
End Class
