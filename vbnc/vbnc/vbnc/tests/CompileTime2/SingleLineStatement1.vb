Class SingleLineStatement1
	Private Raiser As EventRaiser
    Public Event SomeEvent()
    Shared Sub Main()

    End Sub
    Sub Test()
        Dim i As Integer
        Dim loc As Integer
        Dim arr() As Integer

        loc = 100
        i = 1 : i = 2
        loc = 200
        AddHandler raiser.a, AddressOf test : AddHandler raiser.a, AddressOf test
        loc = 300
        Call Test() : Call Test()
        loc = 400
        Select Case i : Case 1 : Return : Case 2 : Return : Case Else : Return : End Select
        loc = 500
        Try : Return : Catch ex As exception : Return : End Try
        loc = 600
        For j As Integer = 0 To 1 : Continue For : Next
        loc = 700
        While True : Continue While : End While
        loc = 800
        Do While True : Continue Do : Loop
        loc = 900
        End : End
        loc = 1000
        Erase arr : Erase arr
        loc = 1100
        Error 1 : Error 2
        loc = 1200
        Exit Sub : Exit Sub
        loc = 1300
label:  test() : test()
        loc = 1400
1:      test() 'numericlabel
        loc = 1500
        GoTo label : GoTo 1
        loc = 1600
        'On Error GoTo 1 : On Error Resume Next
        RaiseEvent someevent() : RaiseEvent someevent()
        loc = 1700
        ReDim arr(2) : ReDim arr(3)
        loc = 1800
        RemoveHandler raiser.a, AddressOf test : RemoveHandler raiser.a, AddressOf test
        loc = 1900
        'Resume Next : Resume Next
        Return : Return
        loc = 2000
        Stop : Stop
        loc = 2100
        SyncLock Me : Return : End SyncLock
        loc = 2200
        Throw New exception() : Throw New exception
        loc = 2300
        With i : Return : End With
        loc = 2400
    End Sub

	Class EventRaiser
		Event a()
	End Class
End Class