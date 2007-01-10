Public Class SingleLineIfWithNonBlockStatements1
    Event EventTest()
    Private m_Field As SingleLineIfWithNonBlockStatements1
    Sub Test()
        Dim arrvar() As Integer
        Dim loc As Integer

        loc = 100
        If True Then AddHandler m_Field.EventTest, AddressOf Me.Test
        loc = 200
        If True Then Call test()
        loc = 300
        If True Then For i As Integer = 0 To 1 : Continue For : Next
        loc = 400
        If True Then test() Else test()
        loc = 500
        If True Then Erase arrvar
        loc = 600
        If True Then Error 1
        loc = 700
        If True Then Exit Sub
        loc = 800
        If True Then GoTo nowhere
        loc = 900
        If True Then On Error GoTo nowhere
        loc = 1000
        If True Then RaiseEvent eventtest()
        loc = 1100
        If True Then ReDim arrvar(1)
        loc = 1200
        If True Then Resume Next
        loc = 1300
        If True Then Return
        loc = 1400
        If True Then Stop
        loc = 1500
        If True Then Throw New system.exception
        loc = 1600
nowhere:
    End Sub
End Class